using System.Collections.Generic;
using GTR.Core.DeckManagement;
using GTR.Core.Engine;
using GTR.Core.ManipulatableRules;
using GTR.Core.ManipulatableRules.Actions;
using GTR.Core.Model;
using GTR.Core.Services;

namespace GTR.Core.Game
{
    public class GameFactory
    {
        private static OrderDeck CreateOrderDeck(IDeckIo deckIo, IResourceProvider resourceProvider, GameOptions gameOptions)
        {
            var deckSerialization = deckIo.GetBuiltinDeck(gameOptions.DeckName);
            var deckType = DeckTypeSerializer.Deserialize(deckSerialization);
            var cardManager = new CardManager(resourceProvider, deckIo);
            var orderDeck = cardManager.CreateOrderCardDeck(deckType);
            return orderDeck;
        }

        public static Game MakeGame(IEnumerable<string> playerInputs,
            GameOptions gameOptions, IDeckIo deckIo, IResourceProvider resourceProvider, IMessageProvider messageProvider)
        {
            var orderDeck = CreateOrderDeck(deckIo, resourceProvider, gameOptions);
            var jackDeck = CreateJackDeck();
            var table = new GameTable(orderDeck, jackDeck);
            var players = CreatePlayers(playerInputs, table);
            table.AddPlayers(players);

            Game game = new Game()
            {
                GameOptions = gameOptions,
                GameTable = table,
                TurnNumber = 1
            };
            return game;
        }

        private static List<Player> CreatePlayers(IEnumerable<string> playerNames, GameTable gameTable)
        {
            var players = new List<Player>();
            foreach (var playerName in playerNames)
            {
                var player = new Player(playerName);
             
                players.Add(player);
            }
        
            return players;
        }

        private static JackDeck CreateJackDeck()
        {
            const int defaultJackCount = 6;

            HashSet<JackCardModel> cards = new HashSet<JackCardModel>();
            for (int i = 0; i < defaultJackCount; i++)
            {
                cards.Add(new JackCardModel());
            }
            return new JackDeck(cards);
        }
    }
}