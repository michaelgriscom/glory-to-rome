#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model.CardCollections;
using GTR.Core.Moves;

#endregion

namespace GTR.Core.Model
{
    public class GameTable : ObservableModel
    {
        private JackDeck _jackDeck;
        private OrderDeck _orderDeck;
        private ObservableCollection<Player> _players;
        private Pool _pool;
        private ObservableCollection<SiteDeck> _siteDecks;

        public GameTable()
        {
            Pool = new Pool();
            OrderDeck = new OrderDeck();
            JackDeck = new JackDeck();
            SiteDecks = new ObservableCollection<SiteDeck>();
        }

        public ObservableCollection<SiteDeck> SiteDecks
        {
            get { return _siteDecks; }
            set
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
    }
}