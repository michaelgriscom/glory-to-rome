#region

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GTR.Core.CardCollections;
using GTR.Core.ManipulatableRules;
using GTR.Core.ManipulatableRules.Actions;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Game
{
    public class Player : ObservableObject
    {
        private readonly Queue<MoveSpace> _availableMoves;
        private readonly LeadFollowManager _lfManager;
        private readonly CardSourceTarget<HandCardModel> _playArea = new CardSourceTarget<HandCardModel>();
        private CompletedBuildings _completedBuildings;
        private ConstructionZone _constructionZone;
        private DemandArea _demandArea;
        private GameTable _gameTable;
        private LeaderCardLocation _leaderCardLocation;
        internal OrderActions PlayerActions;

        internal Player(string playerName, IPlayerInput inputService)
        {
            PlayerName = playerName;
            InputService = inputService;

            Hand = new Hand();
            Camp = new Camp();

            _availableMoves = new Queue<MoveSpace>();
            OutstandingActions = new Multiset<RoleType>();
            _lfManager = new LeadFollowManager(inputService);

            ConstructionZone = new ConstructionZone();
            CompletedBuildings = new CompletedBuildings();
            LeaderCardLocation = new LeaderCardLocation();
            DemandArea = new DemandArea();
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
            private set
            {
                _constructionZone = value;
                RaisePropertyChanged();
            }
        }

        public LeaderCardLocation LeaderCardLocation
        {
            get { return _leaderCardLocation; }
            private set
            {
                _leaderCardLocation = value;
                RaisePropertyChanged();
            }
        }

        public DemandArea DemandArea
        {
            get { return _demandArea; }
            private set
            {
                _demandArea = value;
                RaisePropertyChanged();
            }
        }

        public Multiset<RoleType> OutstandingActions { get; private set; }

        internal ThinkerAction Thinker
        {
            get { return PlayerActions.Thinker; }
        }

        public IPlayerInput InputService { get; private set; }
        public string PlayerName { get; private set; }
        internal Camp Camp { get; private set; }
        internal Hand Hand { get; private set; }

        internal CardSourceTarget<HandCardModel> PlayArea
        {
            get { return _playArea; }
        }

        internal Player PlayerToLeft { get; set; }
        internal Player PlayerToRight { get; set; }

        public void SitAt(GameTable gameTable)
        {
            _gameTable = gameTable;
            PlayerActions = new OrderActions(this, gameTable);
            ConstructionManager constructionManager = new ConstructionManager(this, gameTable);
        }

        public OrderActionBase GetAction(RoleType role)
        {
            return PlayerActions.GetAction(role);
        }

        public int RemainingActions()
        {
            return OutstandingActions.TotalCount;
        }

        internal void AddMoveSpace(MoveSpace movespace)
        {
            _availableMoves.Enqueue(movespace);
        }

        internal void ClearPlayArea()
        {
            while (PlayArea.Count > 0)
            {
                var card = PlayArea.ElementAt(0);
                if (card is JackCardModel)
                {
                    JackCardModel jack = card as JackCardModel;
                    _gameTable.MoveCard(
                        jack,
                        PlayArea,
                        _gameTable.JackDeck);
                }
                else
                {
                    OrderCardModel order = card as OrderCardModel;
                    _gameTable.MoveCard(
                        order,
                        PlayArea,
                        _gameTable.Pool);
                }
            }
        }

        #region lead follow

        internal RoleType? Lead()
        {
            if (Hand.Count == 0)
            {
                ExecuteAction(Thinker);
                return null;
            }
            ActionType action = InputService.GetLead();
            if (action == ActionType.HandPlay)
            {
                var leadCards = _lfManager.GetLeadCards(Hand);
                PlayCards(leadCards);
                var lead = _lfManager.GetLeadRole(leadCards);
                AddActionLead(lead);
                return lead;
            }
            ExecuteAction(Thinker);
            return null;
        }

        internal void Follow(RoleType order)
        {
            if (Hand.Count == 0)
            {
                ExecuteAction(Thinker);
                return;
            }
            var action = InputService.GetFollow();
            if (action == ActionType.Thinker)
            {
                ExecuteAction(Thinker);
            }
            else
            {
                var followCards = _lfManager.GetFollowCards(Hand, order);
                bool followed = (followCards.Count == 0);

                AddActionFollow(order, followed);
                PlayCards(followCards);
            }
        }

        private void AddActionLead(RoleType role)
        {
            OutstandingActions.Add(role);
            foreach (OrderCardModel client in Camp.Clientele)
            {
                if (client.RoleType == role)
                {
                    OutstandingActions.Add(role);
                }
            }
        }

        private void AddActionFollow(RoleType role, bool followed)
        {
            if (followed)
            {
                OutstandingActions.Add(role);
            }
            foreach (OrderCardModel client in Camp.Clientele)
            {
                if (client.RoleType == role)
                {
                    OutstandingActions.Add(role);
                }
            }
        }

        private void PlayCards(IEnumerable<HandCardModel> cards)
        {
            foreach (HandCardModel card in cards)
            {
                _gameTable.MoveCard(
                    card,
                    Hand,
                    PlayArea);
            }
        }

        #endregion lead follow

        #region action

        internal void TakeActions()
        {
            while (OutstandingActions.TotalCount > 0)
            {
                var action = InputService.GetRole(OutstandingActions.UniqueItems);
                OrderActionBase orderAction = GetAction(action);
                ExecuteAction(orderAction);
                OutstandingActions.Remove(action);
            }
        }

        private void ExecuteAction(OrderActionBase action)
        {
            var preMoves = action.Begin();
            foreach (var moveSpace in preMoves)
            {
                EvaluateMoveSpace(moveSpace);
            }

            var moves = action.Execute();
            var move = EvaluateMoveSpace(moves);

            var postMoves = action.Complete(move);
            foreach (var moveSpace in postMoves)
            {
                EvaluateMoveSpace(moveSpace);
            }
        }

        private MoveCombo EvaluateMoveSpace(MoveSpace moveSpace)
        {
            var move = InputService.GetMove(moveSpace);

            Debug.Assert(!(move == null && moveSpace.IsRequired), "player must choose a move");
            _gameTable.ExecuteMove(move);
            return move;
        }

        public int GetActions(RoleType role)
        {
            return OutstandingActions.Count(role);
        }

        #endregion action
    }
}