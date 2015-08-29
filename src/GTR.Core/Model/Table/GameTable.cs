#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GTR.Core.Action;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Serialization;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class GameTable : ObservableModel
    {
        private const int BuildingSiteCount = 6;
        private JackDeck _jackDeck;
        private OrderDeck _orderDeck;
        private ObservableCollection<Player> _players;
        private Pool _pool;
        private ObservableCollection<SiteDeck> _siteDecks;

        public GameTable(OrderDeck orderDeck, JackDeck jackDeck)
        {
            OrderDeck = orderDeck;
            JackDeck = jackDeck;
            JackDeck.Id = "Jack Deck";
            Pool = new Pool();
        }

        public ObservableCollection<SiteDeck> SiteDecks
        {
            get { return _siteDecks; }
            private set
            {
                _siteDecks = value;
                RaisePropertyChanged();
            }
        }

        public JackDeck JackDeck
        {
            get { return _jackDeck; }
            set
            {
                _jackDeck = value;
                RaisePropertyChanged();
            }
        }

        public OrderDeck OrderDeck
        {
            get { return _orderDeck; }
            set
            {
                _orderDeck = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                RaisePropertyChanged();
            }
        }

        public Pool Pool
        {
            get { return _pool; }
            set
            {
                _pool = value;
                RaisePropertyChanged();
            }
        }

        public void AddPlayers(IList<Player> players)
        {
            Players = new ObservableCollection<Player>(players);
            int playerCount = players.Count;
            for (int i = 0; i < playerCount; i++)
            {
                int rightPlayer = (i + 1)%playerCount;
                players[i].PlayerToRight = players[rightPlayer];
                players[rightPlayer].PlayerToLeft = players[i];
            }
            CreateBuildingSites(players.Count);
        }

        public void AddPlayer(Player player)
        {
            if (Players == null)
            {
                Players = new ObservableCollection<Player>();
            }
            Players.Add(player);
        }

        public int GetSiteCost(MaterialType materialType)
        {
            var siteDeck = GetSiteDeck(materialType);
            return siteDeck.Top.SiteType == SiteType.InsideRome ? 1 : 2;
        }

        internal Deck<BuildingSite> GetSiteDeck(MaterialType materialType)
        {
            var siteDeck = SiteDecks.First(deck => materialType == deck.MaterialType);
            return siteDeck;
        }

        /// <summary>
        ///     Get the player at a given location.
        /// </summary>
        /// <param name="location">Player location, indexed from zero.</param>
        /// <returns>Player at the given location.</returns>
        private Player GetPlayerAtLocation(int location)
        {
            Debug.Assert(-1 < location && location < Players.Count, "Invalid player index");
            return Players[location];
        }

        private void CreateBuildingSites(int playerCount)
        {
            int inTownSiteCount = Math.Min(playerCount, BuildingSiteCount);
            int outOfTownSiteCount = BuildingSiteCount - inTownSiteCount;

            var materialTypes = Enum.GetValues(typeof (MaterialType));
            _siteDecks = new ObservableCollection<SiteDeck>();
            foreach (MaterialType type in materialTypes)
            {
                SiteDeck buildingSiteDeck
                    = CreateSiteDeck(type, inTownSiteCount, outOfTownSiteCount);
                SiteDecks.Add(buildingSiteDeck);
            }
        }

        private SiteDeck CreateSiteDeck(MaterialType materialType, int inTownSiteCount, int outOfTownSiteCount)
        {
            SiteDeck siteDeck = new SiteDeck(materialType);
            // place out of site cards on bottom
            for (int i = 0; i < outOfTownSiteCount; i++)
            {
                BuildingSite siteCard = new BuildingSite(materialType, SiteType.OutOfTown);
                siteDeck.AddToTop(siteCard);
            }
            // place in town site cards on top
            for (int i = 0; i < inTownSiteCount; i++)
            {
                BuildingSite siteCard = new BuildingSite(materialType, SiteType.InsideRome);
                siteDeck.AddToTop(siteCard);
            }
            return siteDeck;
        }

        internal void ExecuteMove(IAction action)
        {
            action.Perform();
        }
    }
}