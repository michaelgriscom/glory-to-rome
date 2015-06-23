#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Game
{
    public class GameTable : ObservableObject
    {
        private const int BuildingSiteCount = 6;
        private ObservableCollection<SiteDeck> _siteDecks;

        internal GameTable(Deck<OrderCardModel> orderDeck, CardSourceTarget<JackCardModel> jackDeck)
        {
            OrderDeck = orderDeck;
            JackDeck = jackDeck;
            JackDeck.LocationName = "Jack Deck";
            Pool = new Pool();
            LeaderCard = new LeaderCardModel();
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

        internal CardSourceTarget<JackCardModel> JackDeck { get; private set; }
        internal LeaderCardModel LeaderCard { get; private set; }
        internal Deck<OrderCardModel> OrderDeck { get; private set; }
        internal IList<Player> Players { get; private set; }
        internal Pool Pool { get; private set; }

        public void AddPlayers(IList<Player> players)
        {
            Players = players;
            int playerCount = players.Count;
            for (int i = 0; i < playerCount; i++)
            {
                players[i].SitAt(this);
                int rightPlayer = (i + 1)%playerCount;
                players[i].PlayerToRight = players[rightPlayer];
                players[rightPlayer].PlayerToLeft = players[i];
            }
            CreateBuildingSites(players.Count);
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

        /// <summary>
        ///     Get the location of a player.
        /// </summary>
        /// <param name="player">Player in question.</param>
        /// <returns>Player location, indexed from zero.</returns>
        private int GetPlayerLocation(Player player)
        {
            int playerLocation;
            playerLocation = Players.IndexOf(player);
            Debug.Assert(playerLocation > -1, "Player not in the game");
            return playerLocation;
        }

        internal Player PlayerToLeft(Player player)
        {
            int playerIndex;
            playerIndex = GetPlayerLocation(player);

            // modulo will wrap around if player is at end
            int leftPlayerLocation;
            leftPlayerLocation = (playerIndex - 1)%Players.Count;
            return GetPlayerAtLocation(leftPlayerLocation);
        }

        internal Player PlayerToRight(Player player)
        {
            int playerIndex = GetPlayerLocation(player);

            // modulo will wrap around if player is at end
            int rightPlayerLocation = (playerIndex + 1)%Players.Count;
            return GetPlayerAtLocation(rightPlayerLocation);
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