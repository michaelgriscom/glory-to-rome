#region

using System.Collections.Generic;
using System.Collections.Specialized;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    internal class LegionnaireAction : OrderActionBase
    {
        private readonly CardSourceTarget<OrderCardModel> _demandArea;
        private readonly Hand.OrderCardGroup _playerHand;

        public LegionnaireAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _playerHand = player.Board.Hand.OrderCards;
            _demandArea = player.Board.DemandArea;
            player.Board.DemandArea.CollectionChanged += DemandAreaOnCollectionChanged;
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
            foreach (var handCard in demandee.Board.Hand.OrderCards)
            {
                if (handCard.RoleType == demandedRoleType)
                {
                    IMove<OrderCardModel> demandMove = new Move<OrderCardModel>(handCard, demandee.Board.Hand.OrderCards,
                        Player.Board.Camp.Stockpile);
                    moveSpace.Add(demandMove);
                }
            }
            demandee.InputService.GetMove(moveSpace);
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
            IMove<CardModelBase> undemandMove = new Move<OrderCardModel>(demandedCard, _demandArea,
                _playerHand);

            moveSpace.Add(undemandMove);
            movespaces.Add(moveSpace);
            return movespaces;
        }
    }
}