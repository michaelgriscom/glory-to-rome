#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using GTR.Core.AIController;
using GTR.Core.Game;
using GTR.Core.Marshalling;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Moves;
using GTR.Core.Services;
using GTR.Universal.Services;
using GTR.Windows.Services;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using HttpMethod = System.Net.Http.HttpMethod;

#endregion

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GTR.Universal
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DelayedPlayerInput _delayedPlayerInput;
        private Game game;
        private GameLoader gameLoader;

        public MainPage()
        {
            InitializeComponent();
            //GameViewModel vm = new GameViewModel();
            //DesignGameTable dgt = new DesignGameTable();
            //vm.GameTable = dgt;
            //this.DataContext = vm;


            int playerCount = 3;
            var deckIo = new DeckIo();
            var resourceProvider = new ResourceProvider();
            var messageProvider = new MessageProvider();

            _delayedPlayerInput = new DelayedPlayerInput();

       
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;

           gameLoader = new GameLoader();
           var gameDto = await gameLoader.CreateGame();
            game = gameLoader.LoadGame(gameDto);
            this.DataContext = game;
            await gameLoader.PollForMoves(game, this);

            //await gameLoader.UpdateMoves(game);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           await gameLoader.TestMoves(game);
        }
    }

    public class GameLoader
    {
        MobileServiceClient _client;
        const string endpoint = "http://localhost:51291/";

        private MobileServiceCollection<MoveSerialization> moves;
        private MoveMarshaller<OrderCardModel> orderMoveMarshaller;
        private MoveMarshaller<JackCardModel> jackMoveMarshaller;
        private MoveMarshaller<BuildingSite> foundationMoveMarshaller;
        private ICardLocator cardLocator;
        private ICardCollectionLocator collectionLocator;
        private MoveMaker _moveMaker;

        public GameLoader()
        {
           _client = new MobileServiceClient(endpoint);
            _moveMaker = new MoveMaker();
        }

        public async Task<GameDto> CreateGame()
        {
            GameOptions gameOptions = new GameOptions
            {
                DeckName = "republic",
                MaxPlayers = 2
            };

            CreateGameRequest gr = new CreateGameRequest()
            {
                GameOptions = gameOptions
            };

          var createGameResponse =  await _client.InvokeApiAsync<CreateGameRequest, CreateGameResponseSerialization>("matchmaking", gr);
            var gameId = createGameResponse.GameId;

            var startGameParams = new Dictionary<string, string>() { { "GameId", gameId}};
            var startGameResponse = await _client.InvokeApiAsync<string, StartGameResponseSerialization>("matchmaking", null, HttpMethod.Get, startGameParams);

            return startGameResponse.Game;
        }

        private HashSet<string> CompletedMoves = new HashSet<string>(); 

        public async Task UpdateMoves(Game game)
        {
            var gameId = game.Id;
            var parameters = new Dictionary<string, string>() { { "gameId", gameId } };

            var moves =
                await
                    _client.InvokeApiAsync<string, IEnumerable<ExecutedMoveSerialization>>("move", null, HttpMethod.Get,
                        parameters);

            foreach (var move in moves)
            {
                PropagateMove(move.Move);
            }

            //IMobileServiceTable<MoveSerialization> table = _client.GetTable<MoveSerialization>();
            //await _client.InvokeApiAsync<MoveSerialization, object>("move", playerScore);

            //moves = await table.OrderBy(x => x.CardId).ToCollectionAsync();
        }

        private async Task MakeMove(MoveSerialization move, Game game)
        {
            MoveSetRequest msr = new MoveSetRequest()
            {
                GameId = game.Id,
                MoveSet = new MoveSetSerialization()
                {
                    Moves = new MoveSerialization[]
                    {
                        move
                    }
                }
            };
            await _client.InvokeApiAsync<MoveSetRequest, object>("game", msr);
        }

        public async Task TestMoves(Game game)
        {
            var move = new Move<OrderCardModel>(game.GameTable.OrderDeck.Top, game.GameTable.OrderDeck,
                game.GameTable.Pool);
            var moveDto = orderMoveMarshaller.Marshall(move);
            await MakeMove(moveDto, game);
            await Task.Delay(100);
            await UpdateMoves(game);
        }

        public async Task PollForMoves(Game game, MainPage page)
        {
            while (true)
            {
                await Task.Delay(100);
                await UpdateMoves(game);
            }
        }

        private void PropagateMove(MoveSerialization moveDto)
        {
            if (CompletedMoves.Contains(moveDto.Id))
            {
                return;
            }
            var cardType = cardLocator.DetermineType(moveDto.CardId);
            switch (cardType)
            {
                case CardType.Order:
                    var orderMove = orderMoveMarshaller.UnMarshall(moveDto);
                    _moveMaker.MakeMove(orderMove);
                    break;
                case CardType.Jack:
                    var jackMove = jackMoveMarshaller.UnMarshall(moveDto);
                    _moveMaker.MakeMove(jackMove);
                    break;
                case CardType.BuildingSite:
                    var foundationMove = foundationMoveMarshaller.UnMarshall(moveDto);
                    _moveMaker.MakeMove(foundationMove);
                    break;
            }
            CompletedMoves.Add(moveDto.Id);
        }

        public Game LoadGame(GameDto gameDto)
        {
            var deckIo = new DeckIo();
            var resourceProvider = new ResourceProvider();

            GameMarshaller marshaller = new GameMarshaller(deckIo, resourceProvider);
            var gamePoco = marshaller.UnMarshall(gameDto);

            collectionLocator = gamePoco.GetCardCollectionLocator();
            cardLocator = gamePoco.GetCardLocator();
            orderMoveMarshaller = new MoveMarshaller<OrderCardModel>(cardLocator, collectionLocator);
            jackMoveMarshaller = new MoveMarshaller<JackCardModel>(cardLocator, collectionLocator);
            foundationMoveMarshaller = new MoveMarshaller<BuildingSite>(cardLocator, collectionLocator);

            return gamePoco;
        }
    }
}