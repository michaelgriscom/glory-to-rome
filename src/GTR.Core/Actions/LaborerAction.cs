#region

using GTR.Core.Engine;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Moves;

#endregion

namespace GTR.Core.Actions
{
    internal sealed class LaborerAction : OrderActionBase
    {
        private readonly Stockpile _playerStockpile;
        private readonly Pool _pool;

        public LaborerAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _pool = gameTable.Pool;
            _playerStockpile = Player.Camp.Stockpile;
        }

        protected override MoveSpace GetMoveSpace()
        {
            MoveSpace moveSpace = new MoveSpace();
            foreach (OrderCardModel card in _pool)
            {
                IMove<OrderCardModel> gatherMove = new Move<OrderCardModel>(card, _pool, _playerStockpile);
                moveSpace.Add(gatherMove);
            }
            return moveSpace;
        }
    }
}