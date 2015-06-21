#region

using System.Collections.Generic;
using System.Diagnostics;
using GTR.Core.Action;
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
        private readonly CardSourceTarget<HandCardModel> _playArea;
        private CompletedBuildings _completedBuildings;
        private ConstructionZone _constructionZone;
        private DemandArea _demandArea;
        private GameTable _gameTable;
        private LeaderCardLocation _leaderCardLocation;
        internal OrderActions PlayerActions;

        internal Player(string playerName, IPlayerInput inputService, IMessageProvider messageProvider = null)
        {
            PlayerName = playerName;
            InputService = inputService;
            _playArea = new CardSourceTarget<HandCardModel>(string.Format("Player {0} play area", playerName));

            Hand = new Hand(PlayerName);
            Camp = new Camp(PlayerName);

            _availableMoves = new Queue<MoveSpace>();
            OutstandingActions = new Multiset<RoleType>();
            _lfManager = new LeadFollowManager(inputService);

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
            DisplayAction("joined the game");
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
                    //IMove<JackCardModel> move = new Move<JackCardModel>(jack, PlayArea, _gameTable.JackDeck);
                    _gameTable.MoveCard(
                        jack,
                        PlayArea,
                        _gameTable.JackDeck);
                }
                else
                {
                    OrderCardModel order = card as OrderCardModel;
                    //var move = new Move<OrderCardModel>(order, PlayArea, _gameTable.JackDeck);

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
                DisplayAction("thought");
                ExecuteAction(Thinker);
                return null;
            }
            ActionType action = InputService.GetLead();
            if (action == ActionType.HandPlay)
            {
                var leadCards = _lfManager.GetLeadCards(Hand);
                PlayCards(leadCards);
                var lead = _lfManager.GetLeadRole(leadCards);
                DisplayAction(string.Concat("performed ", lead));
                AddActionLead(lead);
                return lead;
            }
            DisplayAction("thought");

            ExecuteAction(Thinker);
            return null;
        }

        private void DisplayAction(string action)
        {
            Game.MessageProvider.Display(string.Format("Player {0} {1}", PlayerName, action));
        }

        internal void Follow(RoleType leadRole)
        {
            if (Hand.Count == 0)
            {
                ExecuteAction(Thinker);
                DisplayAction("thought");

                return;
            }
            var action = InputService.GetFollow();
            if (action == ActionType.Thinker)
            {
                DisplayAction("thought");

                ExecuteAction(Thinker);
            }
            else
            {
                var followCards = _lfManager.GetFollowCards(Hand, leadRole);
                bool followed = (followCards.Count == 0);
                DisplayAction(string.Concat("followed ", leadRole));

                AddActionFollow(leadRole, followed);
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
                IMove<HandCardModel> move = new Move<HandCardModel>(card, Hand, PlayArea);
                move.Perform();
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
            if (move != null)
            {
                var postMoves = action.Complete(move);
                foreach (var moveSpace in postMoves)
                {
                    EvaluateMoveSpace(moveSpace);
                }
            }
        }

        private IAction EvaluateMoveSpace(MoveSpace moveSpace)
        {
            var move = InputService.GetMove(moveSpace);

            Debug.Assert(!(move == null && moveSpace.IsRequired), "player must choose a move");
            if (move != null)
            {
                _gameTable.ExecuteMove(move);
            }
            return move;
        }

        public int GetActions(RoleType role)
        {
            return OutstandingActions.Count(role);
        }

        #endregion action
    }
}