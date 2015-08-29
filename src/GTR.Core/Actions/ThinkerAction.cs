#region

using System;
using GTR.Core.Action;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    internal class ThinkerAction : OrderActionBase
    {
        private readonly JackDeck _jackDeck;
        private readonly OrderDeck _orderDeck;
        private readonly Hand _playerHand;

        public ThinkerAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _jackDeck = gameTable.JackDeck;
            _orderDeck = gameTable.OrderDeck;
            _playerHand = Player.Hand;
        }

        protected override MoveSpace GetMoveSpace()
        {
            MoveSpace moveSpace = new MoveSpace(true);

            if (_jackDeck.Count > 0)
            {
                JackCardModel topJack = _jackDeck.ElementAt(0);

                Move<JackCardModel> jackThinker = new Move<JackCardModel>(topJack, _jackDeck, _playerHand.JackCards);
                moveSpace.Add(jackThinker);
            }

            var thinkerMove = GetHandThinkerMove();

            moveSpace.Add(thinkerMove);

            return moveSpace;
        }

        private ThinkCombo GetHandThinkerMove()
        {
            ThinkCombo moveSet = new ThinkCombo();

            int maxDraws = Math.Max(1, _playerHand.RefillCapacity - _playerHand.Count);
            int thinkerDraws = Math.Min(maxDraws, _orderDeck.Count);
            for (int i = 0; i < thinkerDraws; i++)
            {
                Move<OrderCardModel> orderThinker = new Move<OrderCardModel>(_orderDeck.ElementAt(i), _orderDeck,
                    _playerHand.OrderCards);
                moveSet.Add(orderThinker);
            }

            return moveSet;
        }
    }
}