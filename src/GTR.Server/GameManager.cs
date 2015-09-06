using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GTR.Core.Game;
using GTR.Core.Marshalling;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Server.Services;
using GTR.Server.DataObjects;
using GTR.Server.Services;
using Microsoft.Azure.Mobile.Server;
using tiberService.Models;
using System.Linq;

namespace GTR.Server
{
    public class GameManager
    {
        private static  GameManager instance;

        public static  GameManager Instance
        {
            get { return instance ?? (instance = new GameManager()); }
        }

        private ConcurrentDictionary<string, GameEngine> Games;
         
        private GameManager()
        {
            deckIo = new DeckIo();
            resourceProvider = new ResourceProvider();
            marshaller = new GameMarshaller(deckIo, resourceProvider);

            Games = new ConcurrentDictionary<string, GameEngine>();    
        }

        private IDeckIo deckIo;
        private IResourceProvider resourceProvider;
        private GameMarshaller marshaller;

        public GameDto CreateGame(GameEntity gameInfo)
        {
            GameFactory gameFactory = new GameFactory();
            var playerIds = gameInfo.Players.Select(p => p.Id);
            Core.Model.Game game = gameFactory.MakeGame(playerIds, gameInfo.GameOptions, deckIo, resourceProvider);
            game.Id = gameInfo.Id;
            PlayerInputService inputService = new PlayerInputService();

            GameEngine gameEngine = new GameEngine(game, new NullMessageProvider(), inputService);

            bool success = Games.TryAdd(game.Id, gameEngine);
            if (!success)
            {
                throw new ArgumentException("Duplicated game");
            }

            return marshaller.Marshall(game);
        }

        public Core.Model.Game GetGameInfo(string gameId)
        {
            if (!Games.ContainsKey(gameId))
            {
                return null;
            }
            else
            {
                return Games[gameId].Game;
            }
        }

        public GameDto GetGameDtoInfo(string gameId)
        {
            var game = GetGameInfo(gameId);
            return marshaller.Marshall(game);
        }

        public async Task<CompletedGame> RunGame(string gameId)
        {
            var gameEngine = Games[gameId];
           return await gameEngine.PlayGame();
        }
    }

    internal class ServerGame
    {
        private GameEngine game;
        private GtrDbContext context;

        public ServerGame(Core.Model.Game game, GtrDbContext context)
        {
            this.game = new GameEngine(game, null, null);
            this.context = context;
        }

        public async void Start()
        {
            await game.PlayGame();

        }
    }
}