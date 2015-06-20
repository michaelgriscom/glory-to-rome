#region

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using GTR.Core.AIController;
using GTR.Core.CardCollections;
using GTR.Core.DeckManagement;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Game
{
    public class Game
    {
        private readonly IDeckIo _deckIo;
        private readonly GameOptions _gameOptions;
        private readonly GameTable _gameTable;
        private readonly IResourceProvider _resourceProvider;
        private Player _actionPlayer; // player who has an action pending
        private Player _leadPlayer; // player whose turn it is to lead

        public Game(
            int playerCount,
            GameOptions gameOptions,
            IDeckIo deckIo,
            IResourceProvider resourceProvider)
        {
            _gameOptions = gameOptions;
            _deckIo = deckIo;
            _resourceProvider = resourceProvider;

            _gameTable = CreateGameTable();
            var players = CreatePlayers(playerCount, _gameTable);
            _gameTable.AddPlayers(players);

            _gameTable.OrderDeck.Cards.CollectionChanged += OrderDeckOnCollectionChanged;
            foreach (var siteDeck in _gameTable.SiteDecks)
            {
                siteDeck.Cards.CollectionChanged += SiteDeckOnCollectionChanged;
            }
            GameOver += OnGameOver;
        }

        private void OnGameOver(object sender, GameOverEventArgs args)
        {
            var gameScore = GameScorer.Score(_gameTable.Players);
            var winners = GameScorer.CalculateWinners(gameScore);

            if (GameWon != null)
            {
                GameWonEventArgs gameWonArgs = new GameWonEventArgs
                {
                    Winners = winners,
                    GameScore = gameScore
                };
                GameWon(this, gameWonArgs);
            }
        }

        private void SiteDeckOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Remove)
            {
                return;
            }

            bool areAvailableSites = _gameTable.SiteDecks.Any(deck => deck.Top.SiteType == SiteType.InsideRome);
            if (areAvailableSites)
            {
                return;
            }
            if (GameOver != null)
            {
                GameOverEventArgs gameOverEventArgs = new GameOverEventArgs {Reason = Messages.ZeroSitesGameOver};
                GameOver(this, gameOverEventArgs);
            }
        }

        internal event GameOverHandler GameOver;
        internal event GameWonHandler GameWon;

        private void OrderDeckOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Remove)
            {
                return;
            }
            if (_gameTable.OrderDeck.Cards.Count != 0)
            {
                return;
            }
            if (GameOver != null)
            {
                GameOverEventArgs gameOverEventArgs = new GameOverEventArgs {Reason = Messages.DeckEmptyGameOver};
                GameOver(this, gameOverEventArgs);
            }
        }

        private GameTable CreateGameTable()
        {
            var jackDeck = CreateJackDeck();
            var deckSerialization = _deckIo.GetBuiltinDeck(_gameOptions.DeckName);
            var deckType = DeckTypeSerializer.Deserialize(deckSerialization);
            var cardManager = new CardManager(_resourceProvider, _deckIo);
            var orderDeck = cardManager.CreateOrderCardDeck(deckType);

            var table = new GameTable(orderDeck, jackDeck);
            return table;
        }

        public Player PlayGame()
        {
            DealCards();
            _leadPlayer = DetermineGoesFirst(_gameTable);
            HandleTurn();
            Player winner = _leadPlayer;
            return winner;
        }

        private void DealCards()
        {
            foreach (var player in _gameTable.Players)
            {
                for (int i = 0; i < player.Hand.RefillCapacity; i++)
                {
                    var topDeckCard = _gameTable.OrderDeck.Top;
                    _gameTable.MoveCard(topDeckCard, _gameTable.OrderDeck, player.Hand.OrderCards);
                }
                var topJackCard = _gameTable.JackDeck.ElementAt(0);
                _gameTable.MoveCard(topJackCard, _gameTable.JackDeck, player.Hand.JackCards);
            }
        }

        private void HandleTurn()
        {
            HandleLeadFollow();
            HandleActions();
            CompleteTurn();
        }

        private void CompleteTurn()
        {
            foreach (Player player in _gameTable.Players)
            {
                player.ClearPlayArea();
            }
            TransferLeaderCard(_leadPlayer.PlayerToRight);
        }

        private void HandleActions()
        {
            _actionPlayer = _leadPlayer;
            for (int playerNumber = 0; playerNumber < _gameTable.Players.Count - 1; playerNumber++)
            {
                _actionPlayer.TakeActions();
                _actionPlayer = _actionPlayer.PlayerToRight;
            }
        }

        private void HandleLeadFollow()
        {
            _actionPlayer = _leadPlayer;

            var leadRole = _leadPlayer.Lead();
            if (leadRole == null)
            {
                return;
            }
            RoleType lead = (RoleType) leadRole;

            for (int playerNumber = 0; playerNumber < _gameTable.Players.Count - 1; playerNumber++)
            {
                _actionPlayer = _actionPlayer.PlayerToRight;
                _actionPlayer.Follow(lead);
            }
        }

        private void TransferLeaderCard(Player player)
        {
            if (_leadPlayer != null)
            {
                _gameTable.MoveCard(
                    _gameTable.LeaderCard,
                    _leadPlayer.LeaderCardLocation,
                    player.LeaderCardLocation);
                _leadPlayer.LeaderCardLocation.Remove(_gameTable.LeaderCard);
            }
            player.LeaderCardLocation.Add(_gameTable.LeaderCard);
        }

        private static CardSourceTarget<JackCardModel> CreateJackDeck()
        {
            const int defaultJackCount = 6;

            HashSet<JackCardModel> cards = new HashSet<JackCardModel>();
            for (int i = 0; i < defaultJackCount; i++)
            {
                cards.Add(new JackCardModel());
            }
            return new CardSourceTarget<JackCardModel>(cards);
        }

        private static List<Player> CreatePlayers(int playerCount, GameTable gameTable)
        {
            IPlayerInput playerInput = new AiPlayerInput();
            List<Player> players = new List<Player>(playerCount);
            for (int i = 0; i < playerCount; i++)
            {
                string playerName = "Player " + (i + 1);
                players.Add(new Player(playerName, playerInput));
            }
            return players;
        }

        private Player DetermineGoesFirst(GameTable playingField)
        {
            return DrawForFirst(
                playingField.Players,
                playingField.OrderDeck,
                playingField.Pool);
        }

        private Player DrawForFirst(IList<Player> players, Deck<OrderCardModel> orderDeck, Pool pool)
        {
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
    }
}