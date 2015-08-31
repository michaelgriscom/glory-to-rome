#region

using System.Collections.Generic;
using System.Collections.Specialized;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Moves;

#endregion

namespace GTR.Core.Actions
{
    internal class LegionnaireAction : OrderActionBase
    {
        private readonly ObservableCardCollection<OrderCardModel> _demandArea;
        private readonly Hand.OrderCardGroup _playerHand;

        public LegionnaireAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _playerHand = Player.Hand.OrderCards;
            _demandArea = Player.DemandArea;
            Player.DemandArea.CollectionChanged += DemandAreaOnCollectionChanged;
        }

        private void DemandAreaOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }
            var demandedCards = notifyCollectionChangedEventArgs.NewItems;
            foreach (OrderCardModel card in demandedCards)
            {
                HandleDemand(card);
            }
        }

        private void HandleDemand(OrderCardModel card)
        {
            var demandedCardType = card.RoleType;
            DemandCard(demandedCardType, Player.PlayerToLeft);
            DemandCard(demandedCardType, Player.PlayerToRight);
        }

        private void DemandCard(RoleType demandedRoleType, Player demandee)
        {
            MoveSpace moveSpace = new MoveSpace(true);
            foreach (var handCard in demandee.Hand.OrderCards)
            {
                if (handCard.RoleType == demandedRoleType)
                {
                    IMove<OrderCardModel> demandMove = new Move<OrderCardModel>(handCard, demandee.Hand.OrderCards,
                        Player.Camp.Stockpile);
                    moveSpace.Add(demandMove);
                }
            }
            // TODO: implement
            //demandee.InputService.GetMove(moveSpace);
        }

        protected override MoveSpace GetMoveSpace()
        {
            var moveSpace = new MoveSpace();
            foreach (var card in _playerHand)
            {
                IMove<OrderCardModel> demandMove = new Move<OrderCardModel>(card, _playerHand, _demandArea);
                moveSpace.Add(demandMove);
            }
            return moveSpace;
        }

        protected override IEnumerable<MoveSpace> GetPostmoveSpaces(IAction action)
        {
            var movespaces = new List<MoveSpace>();
            var moveSpace = new MoveSpace(true);

            //var demandEnumeration = demandArea.GetEnumerator();
            //OrderCard demandCard = (OrderCard)demandEnumeration.Current;
            var demandedCard = ((IMove<OrderCardModel>) action).Card;
            var undemandMove = new Move<OrderCardModel>(demandedCard, _demandArea,
                _playerHand);

            moveSpace.Add(undemandMove);
            movespaces.Add(moveSpace);
            return movespaces;
        }
    }
}