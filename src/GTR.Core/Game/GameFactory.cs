#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GTR.Core.DeckManagement;
using GTR.Core.Engine;
using GTR.Core.Marshalling;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Services;

#endregion

namespace GTR.Core.Game
{
    public class GameFactory
    {
        private const int BuildingSiteCount = 6;
        CardFactory cardFactory;

        public GameFactory()
        {
            cardFactory = new CardFactory();
        }

        public Model.Game MakeGame(
            IEnumerable<string> playerIds,
            GameOptions gameOptions,
            IDeckIo deckIo,
            IResourceProvider resourceProvider)
        {
            var deckSerialization = deckIo.GetBuiltinDeck(gameOptions.DeckName);
            var deckType = DeckTypeSerializer.Deserialize(deckSerialization);

            var cardManager = new CardManager(resourceProvider, deckIo, cardFactory);
            var orderDeck = cardManager.CreateOrderCardDeck(deckType);

            var jackDeck = CreateJackDeck();
            var table = new GameTable
            {
                OrderDeck = orderDeck,
                JackDeck = jackDeck,
            };

            var players = CreatePlayers(playerIds);
            table.AddPlayers(players);

            var buildingSites = CreateBuildingSites(players.Count);
            table.SiteDecks = new ObservableCollection<SiteDeck>(buildingSites);

            Model.Game game = new Model.Game
            {
                GameOptions = gameOptions,
                GameTable = table,
                TurnNumber = 1,
            };

            return game;
        }

        private List<Player> CreatePlayers(IEnumerable<string> playerNames)
        {
            var players = new List<Player>();
            foreach (var playerName in playerNames)
            {
                var player = new Player(playerName);
                players.Add(player);
            }

            return players;
        }

        private JackDeck CreateJackDeck()
        {
            const int defaultJackCount = 6;

            HashSet<JackCardModel> cards = new HashSet<JackCardModel>();
            for (int i = 0; i < defaultJackCount; i++)
            {
                var jack = cardFactory.CreateJackCard();
                cards.Add(jack);
            }
            return new JackDeck(cards);
        }

        private IEnumerable<SiteDeck> CreateBuildingSites(int playerCount)
        {
            int inTownSiteCount = Math.Min(playerCount, BuildingSiteCount);
            int outOfTownSiteCount = BuildingSiteCount - inTownSiteCount;

            var materialTypes = Enum.GetValues(typeof(MaterialType));
            var siteDecks = new List<SiteDeck>();
            foreach (MaterialType type in materialTypes)
            {
                SiteDeck buildingSiteDeck
                    = CreateSiteDeck(type, inTownSiteCount, outOfTownSiteCount);
                siteDecks.Add(buildingSiteDeck);
            }

            return siteDecks;
        }

        private SiteDeck CreateSiteDeck(MaterialType materialType, int inTownSiteCount, int outOfTownSiteCount)
        {
            SiteDeck siteDeck = new SiteDeck(materialType);
            // place in town site cards on top
            for (int i = 0; i < inTownSiteCount; i++)
            {
                BuildingSite siteCard = cardFactory.CreateBuildingSite(materialType, SiteType.InsideRome);
                siteDeck.Add(siteCard);
            }
            // place out of site cards on bottom
            for (int i = 0; i < outOfTownSiteCount; i++)
            {
                BuildingSite siteCard = cardFactory.CreateBuildingSite(materialType, SiteType.OutOfTown);
                siteDeck.Add(siteCard);
            }
            return siteDeck;
        }
    }
}