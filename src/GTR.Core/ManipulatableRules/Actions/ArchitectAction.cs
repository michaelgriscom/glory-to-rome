﻿#region

using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Buildings;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    internal sealed class ArchitectAction : BuildOrderActionBase
    {
        private readonly IEnumerable<BuildingSite> _buildingSites;
        private readonly Hand.OrderCardGroup _playerHand;
        private readonly Stockpile _playerStockpile;

        public ArchitectAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _buildingSites = player.ConstructionZone;
            _playerHand = player.Hand.OrderCards;
            _playerStockpile = player.Camp.Stockpile;
        }

        public override RoleType Role
        {
            get { return RoleType.Architect; }
        }

        protected override MoveSpace GetMoveSpace()
        {
            MoveSpace moveSpace = new MoveSpace();
            foreach (OrderCardModel handCard in _playerHand)
            {
                PopulateBuildMoves(handCard, moveSpace);
            }
            foreach (OrderCardModel stockpileCard in _playerStockpile)
            {
                PopulateFeedMoves(stockpileCard, moveSpace);
            }
            return moveSpace;
        }

        protected override void PopulateFeedMoves(OrderCardModel stockpileCard, MoveSpace moveSpace)
        {
            foreach (BuildingSite buildingSite in _buildingSites)
            {
                if (stockpileCard.RoleType.ToMaterial() == buildingSite.MaterialType)
                {
                    IMove<OrderCardModel> feedMove = new Move<OrderCardModel>(stockpileCard, _playerStockpile,
                        buildingSite.Materials);
                    moveSpace.Add(feedMove);
                }
            }
        }
    }
}