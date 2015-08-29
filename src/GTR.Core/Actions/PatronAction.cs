#region

using GTR.Core.Action;
using GTR.Core.Engine;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Actions
{
    internal class PatronAction : OrderActionBase
    {
        private readonly Clientele _playerClientele;
        private readonly Pool _pool;

        public PatronAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _playerClientele = Player.Camp.Clientele;
            _pool = gameTable.Pool;
        }

        protected override MoveSpace GetMoveSpace()
        {
            MoveSpace moveSpace = new MoveSpace();
            bool canHireEvaluated = false;
            foreach (OrderCardModel card in _pool)
            {
                // CONSIDER: we could support card-level rules here if we
                // didn't cash the result, so we could support
                // e.g., a building allows to have one laborer more than the max capacity,
                // but for now we will keep with the more efficient way.
                if (!canHireEvaluated)
                {
                    if (!_playerClientele.CanAdd(card))
                    {
                        break;
                    }
                    canHireEvaluated = true;
                }

                IMove<OrderCardModel> hireMove = new Move<OrderCardModel>(card, _pool, _playerClientele);
                moveSpace.Add(hireMove);
            }
            return moveSpace;
        }
    }
}