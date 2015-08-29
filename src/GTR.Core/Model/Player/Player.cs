#region

using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Engine
{
    public class Player : ObservableModel
    {
        private Camp _camp;
        private CompletedBuildings _completedBuildings;
        private ConstructionZone _constructionZone;
        private DemandArea _demandArea;
        private Hand _hand;
        private PlayArea _playArea;
        private string _playerName;
        private Player _playerToLeft;
        private Player _playerToRight;

        public Player(string playerName)
        {
            PlayArea = new PlayArea(playerName);
            Hand = new Hand(playerName);
            Camp = new Camp(playerName);
            ConstructionZone = new ConstructionZone(playerName);
            CompletedBuildings = new CompletedBuildings(playerName);
            DemandArea = new DemandArea(playerName);
            PlayerName = playerName;
            OutstandingActions = new Multiset<RoleType>();
        }

        public CompletedBuildings CompletedBuildings
        {
            get { return _completedBuildings; }
            set
            {
                _completedBuildings = value;
                RaisePropertyChanged();
            }
        }

        public ConstructionZone ConstructionZone
        {
            get { return _constructionZone; }
            set
            {
                _constructionZone = value;
                RaisePropertyChanged();
            }
        }

        public DemandArea DemandArea
        {
            get { return _demandArea; }
            set
            {
                _demandArea = value;
                RaisePropertyChanged();
            }
        }

        public Camp Camp
        {
            get { return _camp; }
            set
            {
                _camp = value;
                RaisePropertyChanged();
            }
        }

        public Hand Hand
        {
            get { return _hand; }
            set
            {
                _hand = value;
                RaisePropertyChanged();
            }
        }

        public PlayArea PlayArea
        {
            get { return _playArea; }
            set
            {
                _playArea = value;
                RaisePropertyChanged();
            }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                RaisePropertyChanged();
            }
        }

        public Player PlayerToLeft
        {
            get { return _playerToLeft; }
            set
            {
                _playerToLeft = value;
                RaisePropertyChanged();
            }
        }

        public Player PlayerToRight
        {
            get { return _playerToRight; }
            set
            {
                _playerToRight = value;
                RaisePropertyChanged();
            }
        }

        public Multiset<RoleType> OutstandingActions { get; }
    }
}