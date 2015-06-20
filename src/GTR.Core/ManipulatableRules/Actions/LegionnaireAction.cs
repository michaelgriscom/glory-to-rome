#region

using System.Collections.Generic;
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
            _playerHand = player.Hand.OrderCards;
            _demandArea = player.DemandArea;
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

        protected override IEnumerable<MoveSpace> GetPostmoveSpaces(IMove<CardModelBase> move)
        {
            var movespaces = new List<MoveSpace>();
            var moveSpace = new MoveSpace(true);

            //var demandEnumeration = demandArea.GetEnumerator();
            //OrderCard demandCard = (OrderCard)demandEnumeration.Current;
            IMove<CardModelBase> undemandMove = new Move<OrderCardModel>((OrderCardModel) move.Card, _demandArea,
                _playerHand);

            moveSpace.Add(undemandMove);
            movespaces.Add(moveSpace);
            return movespaces;
        }
    }
}