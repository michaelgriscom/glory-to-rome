#region

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.Engine;
using GTR.Core.ManipulatableRules;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Game
{
    public class GameEngine
    {
        private readonly Game gameModel;
        private readonly Dictionary<Player, PlayerEngine> playerEngines;
        private GameOptions _gameOptions;
        private bool _isGameOver;
        private CompletedGame completedGame;
        private IPlayerInputService playerInputService;
        internal event GameOverHandler GameOver = delegate { };

        public GameEngine(
            Game gameModel,
            IMessageProvider messageProvider,
            IPlayerInputService playerInputService)
        {
            this.gameModel = gameModel;
            MessageProvider = messageProvider;
            this.playerInputService = playerInputService;

            WireEvents();

            playerEngines = new Dictionary<Player, PlayerEngine>();
            foreach (var player in GameTable.Players)
            {
                var playerEngine = new PlayerEngine(player, playerInputService, gameModel.GameTable);
                playerEngines.Add(player, playerEngine);
            }
        }

        public Game Game { get { return gameModel; } }

        private void WireEvents()
        {
            GameTable.OrderDeck.Cards.CollectionChanged += OrderDeckOnCollectionChanged;
            foreach (var siteDeck in GameTable.SiteDecks)
            {
                siteDeck.Cards.CollectionChanged += SiteDeckOnCollectionChanged;
            }
            GameOver += OnGameOver;
        }

        public IMessageProvider MessageProvider { get; set; }

        private GameTable GameTable
        {
            get { return gameModel?.GameTable; }
        }

        public async Task<CompletedGame> PlayGame()
        {
            DealCards();
            gameModel.LeadPlayer = DetermineGoesFirst(GameTable);
            gameModel.TurnNumber = 1;

            while (!_isGameOver)
            {
                await HandleTurn();
                gameModel.TurnNumber++;
            }
            return completedGame;
        }

        #region game start
        private void DealCards()
        {
            foreach (var player in GameTable.Players)
            {
                for (int i = 0; i < player.Hand.RefillCapacity; i++)
                {
                    var topDeckCard = GameTable.OrderDeck.Top;
                    var move = new Move<OrderCardModel>(topDeckCard, GameTable.OrderDeck, player.Hand.OrderCards);
                    move.Perform();
                }
                var topJackCard = GameTable.JackDeck.ElementAt(0);
                var jackMove = new Move<JackCardModel>(topJackCard, GameTable.JackDeck, player.Hand.JackCards);
                jackMove.Perform();
            }
        }


        private Player DetermineGoesFirst(GameTable playingField)
        {
            var players = playingField.Players;
            var orderDeck = playingField.OrderDeck;
            var pool = playingField.Pool;

            orderDeck.Shuffle();

            Dictionary<Player, OrderCardModel> playerDraws = new Dictionary<Player, OrderCardModel>();

            foreach (var player in players)
            {
                var drawnCard = orderDeck.Draw();
                pool.Add(drawnCard);
                playerDraws.Add(player, drawnCard);
            }
            // TODO: implement once buildings are done
            return players.ElementAt(0);
        }
        #endregion

        #region game mid
        private async Task HandleTurn()
        {
            MessageProvider.Display(string.Format("Turn {0} lead phase", gameModel.TurnNumber));
            await HandleLeadFollowAsync();
            MessageProvider.Display(string.Format("Turn {0} action phase", gameModel.TurnNumber));
            await HandleActionsAsync();
            CompleteTurn();
            MessageProvider.Display(string.Format("Turn {0} complete", gameModel.TurnNumber));
        }

        private async Task HandleLeadFollowAsync()
        {
            gameModel.ActionPlayer = gameModel.LeadPlayer;
            var leaderEngine = playerEngines[gameModel.LeadPlayer];
            var lead = await leaderEngine.LeadAsync();
            if (lead == null)
            {
                return;
            }

            var leadRole = (RoleType) lead;
            for (int playerNumber = 0; playerNumber < gameModel.GameTable.Players.Count - 1; playerNumber++)
            {
                gameModel.ActionPlayer = gameModel.ActionPlayer.PlayerToRight;
                var actionEngine = playerEngines[gameModel.ActionPlayer];
                await actionEngine.FollowAsync(leadRole);
            }
        }

        private async Task HandleActionsAsync()
        {
            gameModel.ActionPlayer = gameModel.LeadPlayer;
            for (int playerNumber = 0; playerNumber < gameModel.GameTable.Players.Count - 1; playerNumber++)
            {
                var actionEngine = playerEngines[gameModel.ActionPlayer];
                await actionEngine.TakeActionsAsync();
                gameModel.ActionPlayer = gameModel.ActionPlayer.PlayerToRight;
            }
        }

        private void CompleteTurn()
        {
            foreach (Player player in gameModel.GameTable.Players)
            {
                var playerEngine = playerEngines[player];

                playerEngine.ClearPlayArea();
            }
            gameModel.LeadPlayer = gameModel.LeadPlayer.PlayerToRight;
        }
        #endregion

        #region end game
        private void SiteDeckOnCollectionChanged(object sender,
         NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Remove)
            {
                return;
            }

            bool areAvailableSites = GameTable.SiteDecks.Any(deck => deck.Top.SiteType == SiteType.InsideRome);
            if (areAvailableSites)
            {
                return;
            }
            if (GameOver != null)
            {
                GameOverEventArgs gameOverEventArgs = new GameOverEventArgs { Reason = Messages.ZeroSitesGameOver };
                GameOver(this, gameOverEventArgs);
            }
        }

        private void OrderDeckOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Remove)
            {
                return;
            }
            if (GameTable.OrderDeck.Cards.Count != 0)
            {
                return;
            }
            if (GameOver != null)
            {
                GameOverEventArgs gameOverEventArgs = new GameOverEventArgs { Reason = Messages.DeckEmptyGameOver };
                GameOver(this, gameOverEventArgs);
            }
        }

        private void OnGameOver(object sender, GameOverEventArgs args)
        {
            var gameScore = GameScorer.Score(GameTable.Players);
            var winners = GameScorer.CalculateWinners(gameScore);

            completedGame = new CompletedGame
            {
                Winners = winners,
                GameScore = gameScore
            };
            _isGameOver = true;
        }
        #endregion
    }
}