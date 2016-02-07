#region

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GTR.Core.Actions;
using GTR.Core.Buildings;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Moves;
using GTR.Core.Services;

#endregion

namespace GTR.Core.Engine
{
    public class PlayerEngine
    {
        private const int CardsToActAsJack = 3;
        private readonly IPlayerInputService _inputService;
        private GameTable _gameTable;
        private OrderActions _playerActions;
        private IPlayerInput _playerInput;
        private MoveMaker _moveMaker;

        public PlayerEngine(Player player, IPlayerInputService inputService, GameTable gameTable, MoveMaker moveMaker)
        {
            Player = player;
            _inputService = inputService;
            _playerActions = new OrderActions(player, gameTable);
            _moveMaker = moveMaker;
            WireEvents();
        }

        public Player Player { get; }

        internal OrderActions PlayerActions
        {
            get { return PlayerActions; }
        }

        private void WireEvents()
        {
            var constructionZone = Player.ConstructionZone;
            constructionZone.CollectionChanged += ConstructionZoneOnCollectionChanged;

            var completedFoundations = Player.Camp.CompletedFoundations;
            completedFoundations.CollectionChanged += CompletedFoundationsOnCollectionChanged;
        }

        internal void ClearPlayArea()
        {
            while (Player.PlayArea.JackCards.Count > 0)
            {
                var card = Player.PlayArea.JackCards.ElementAt(0);
                _moveMaker.MakeMove(card, Player.PlayArea.JackCards, _gameTable.JackDeck, Player);
            }
            while (Player.PlayArea.OrderCards.Count > 0)
            {
                var card = Player.PlayArea.OrderCards.ElementAt(0);
                _moveMaker.MakeMove(card, Player.PlayArea.OrderCards, _gameTable.Pool, Player);
            }
        }

        #region building construction

        private void CompletedFoundationsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            foreach (BuildingSite buildingSite in notifyCollectionChangedEventArgs.NewItems)
            {
                Player.Camp.InfluencePoints += buildingSite.MaterialType.MaterialWorth();
            }
        }

        private void ConstructionZoneOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }
            foreach (BuildingSite newSite in notifyCollectionChangedEventArgs.NewItems)
            {
                newSite.Complete += BuildingOnComplete;
                BuildingEffectFactory effectService = new BuildingEffectFactory();
                var buildingEffect = effectService.Create(newSite.BuildingFoundation.Building.Name);
                buildingEffect.CompleteBuilding(this, _gameTable);
                buildingEffect.ActivateBuilding(this, _gameTable);
            }
        }

        private void BuildingOnComplete(object sender, BuildingCompletedEventArgs args)
        {
            // move foundation into influence area
            var buildingSite = args.BuildingSite;
            var completedFoundations = Player.Camp.CompletedFoundations;
            var constructionZone = Player.ConstructionZone;
            _moveMaker.MakeMove(buildingSite, constructionZone, completedFoundations, Player);

            // move building into completed building zone
            var buildingCard = args.BuildingSite.BuildingFoundation.Building;
            var completedBuildings = Player.CompletedBuildings;

            _moveMaker.MakeMove(buildingCard, buildingSite.BuildingFoundation,
                completedBuildings, Player);

            // material cards no longer needed,
            // let the garbage collector get rid of them
            buildingSite.Materials = null;

            // TODO, we need to remove these event listeners but that will require rearchitecting
            buildingSite.Complete -= BuildingOnComplete;
        }

        #endregion

        #region validation

        private ICollection<RoleType> GetAvailableLeads(ICollection<HandCardModel> leadCards)
        {
            IList<RoleType> availableLeads = new List<RoleType>();
            if (leadCards.ElementAt(0) is JackCardModel || leadCards.Count > 1)
            {
                availableLeads = EnumExtensions.GetEnumList<RoleType>();
            }
            else
            {
                OrderCardModel orderCard = leadCards.ElementAt(0) as OrderCardModel;
                Debug.Assert(orderCard != null, "orderCard != null");
                availableLeads.Add(orderCard.RoleType);
            }
            return availableLeads;
        }

        private bool IsValidFollow(ICollection<HandCardModel> cards, RoleType role)
        {
            bool isValid = false;
            if (cards == null)
            {
                return false;
            }
            if (cards.Count == 1)
            {
                HandCardModel card = cards.ElementAt(0);
                isValid = card is JackCardModel ||
                          ((OrderCardModel) card).RoleType == role;
            }
            else if (cards.Count == CardsToActAsJack)
            {
                isValid = CardsOfSameRole(cards);
            }
            return isValid;
        }

        private bool IsValidLead(ICollection<HandCardModel> cards)
        {
            bool isValid = false;
            if (cards == null)
            {
                return false;
            }
            if (cards.Count == 1)
            {
                isValid = true;
            }
            else if (cards.Count == CardsToActAsJack)
            {
                isValid = CardsOfSameRole(cards);
            }
            return isValid;
        }

        /// <summary>
        ///     Determines whether the given cards are all of the same role. Jacks are treated similarly to database nulls,
        ///     i.e., false is returned if there are any Jacks.
        /// </summary>
        /// <param name="cards">A collection of cards assumed to contain more than 1.</param>
        private bool CardsOfSameRole(IEnumerable<HandCardModel> cards)
        {
            if (cards == null) throw new ArgumentNullException("cards");
            RoleType role = RoleType.Architect; // assign to value to satisfy compiler
            bool firstIter = true;
            foreach (HandCardModel card in cards)
            {
                OrderCardModel orderCard = card as OrderCardModel;
                if (orderCard == null)
                {
                    return false;
                }
                if (firstIter)
                {
                    role = orderCard.RoleType;
                    firstIter = false;
                }
                else if (orderCard.RoleType != role)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region lead follow

        internal async Task<RoleType?> LeadAsync()
        {
            if (Player.Hand.Count == 0)
            {
                await ExecuteActionAsync(Thinker);
                return null;
            }

            var leadRequest = new LeadRequest
            {
                Player = Player
            };

            var lead = await _inputService.RequestLeadAsync(leadRequest);

            if (lead.IsThink)
            {
                await ExecuteActionAsync(Thinker);
                return null;
            }
            PlayCards(lead.Cards);
            Player.OutstandingActions.Add(lead.Role);
            AddClienteleActions(lead.Role);
            return lead.Role;
        }

        internal async Task FollowAsync(RoleType leadRole)
        {
            if (Player.Hand.Count == 0)
            {
                await ExecuteActionAsync(Thinker);
                return;
            }

            var followRequest = new FollowRequest
            {
                Player = Player,
                RoleType = leadRole
            };

            var follow = await _inputService.RequestFollowAsync(followRequest);

            if (follow.IsThink)
            {
                await ExecuteActionAsync(Thinker);
                AddClienteleActions(leadRole);
            }
            else
            {
                PlayCards(follow.Cards);
                if (follow.Cards.Count > 0)
                {
                    Player.OutstandingActions.Add(leadRole);
                }
                AddClienteleActions(leadRole);
            }
        }

        private void AddClienteleActions(RoleType role)
        {
            foreach (OrderCardModel client in Player.Camp.Clientele)
            {
                if (client.RoleType == role)
                {
                    Player.OutstandingActions.Add(role);
                }
            }
        }

        private void PlayCards(IEnumerable<HandCardModel> cards)
        {
            foreach (HandCardModel card in cards)
            {
                if (card is JackCardModel)
                {
                    var jack = card as JackCardModel;
                    _moveMaker.MakeMove(jack, Player.Hand.JackCards, Player.PlayArea.JackCards, Player);
                }
                else
                {
                    var order = card as OrderCardModel;
                    _moveMaker.MakeMove(order, Player.Hand.OrderCards, Player.PlayArea.OrderCards, Player);
                }
            }
        }

        #endregion lead follow

        #region action

        internal ThinkerAction Thinker
        {
            get { return PlayerActions.Thinker; }
        }

        internal async Task TakeActionsAsync()
        {
            while (Player.OutstandingActions.TotalCount > 0)
            {
                var action = Player.OutstandingActions.First();
                OrderActionBase orderAction = PlayerActions.GetAction(action);
                await ExecuteActionAsync(orderAction);
                Player.OutstandingActions.Remove(action);
            }
        }

        private async Task ExecuteActionAsync(OrderActionBase action)
        {
            var preMoves = action.Begin();
            foreach (var moveSpace in preMoves)
            {
                await EvaluateMoveSpaceAsync(moveSpace);
            }

            var moves = action.Execute();
            var move = await EvaluateMoveSpaceAsync(moves);
            if (move != null)
            {
                var postMoves = action.Complete(move);
                foreach (var moveSpace in postMoves)
                {
                    await EvaluateMoveSpaceAsync(moveSpace);
                }
            }
        }

        private async Task<IAction> EvaluateMoveSpaceAsync(MoveSpace moveSpace)
        {
            var moveRequest = new MoveRequest
            {
                Player = Player,
                MoveSpace = moveSpace
            };

            var move = await _inputService.RequestMoveAsync(moveRequest);
            if (move != null)
            {
                _moveMaker.MakeMove(move);
            }
            return move;
        }

        #endregion action
    }
}