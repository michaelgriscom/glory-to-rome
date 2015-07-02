#region

using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class PlayerBoard : ObservableObject
    {
        private Camp _camp;
        private CompletedBuildings _completedBuildings;
        private ConstructionZone _constructionZone;
        private DemandArea _demandArea;
        private Hand _hand;
        private LeaderCardLocation _leaderCardLocation;
        private PlayArea _playArea;

        public PlayerBoard(string playerName)
        {
            PlayArea = new PlayArea(playerName);
            Hand = new Hand(playerName);
            Camp = new Camp(playerName);
            ConstructionZone = new ConstructionZone(playerName);
            CompletedBuildings = new CompletedBuildings(playerName);
            LeaderCardLocation = new LeaderCardLocation(playerName);
            DemandArea = new DemandArea(playerName);
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

        public LeaderCardLocation LeaderCardLocation
        {
            get { return _leaderCardLocation; }
            set
            {
                _leaderCardLocation = value;
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
    }
}