﻿#region

using GTR.Core.Engine;
using GTR.Core.Model;
using GTR.Core.Moves;

#endregion

namespace GTR.Core.Actions
{
    internal class MerchantAction : OrderActionBase
    {
        private readonly Stockpile _playerStockpile;
        private readonly Vault _playerVault;

        public MerchantAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _playerStockpile = Player.Camp.Stockpile;
            _playerVault = Player.Camp.Vault;
        }

        protected override MoveSpace GetMoveSpace()
        {
            MoveSpace moveSpace = new MoveSpace();
            bool canHireEvaluated = false;
            foreach (OrderCardModel card in _playerStockpile)
            {
                // CONSIDER: we could support card-level rules here if we
                // didn't cash the result, so we could support
                // e.g., a building allows to have one laborer more than the max capacity,
                // but for now we will keep with the more efficient way.
                if (!canHireEvaluated)
                {
                    if (!_playerVault.CanAdd(card))
                    {
                        break;
                    }
                    canHireEvaluated = true;
                }

                IMove<OrderCardModel> hireMove = new Move<OrderCardModel>(card, _playerStockpile, _playerVault);
                moveSpace.Add(hireMove);
            }
            return moveSpace;
        }
    }
}