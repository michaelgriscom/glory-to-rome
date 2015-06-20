#region

using System.Collections.Generic;
using GTR.Core.Buildings;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    internal sealed class CraftsmanAction : BuildOrderActionBase
    {
        private readonly IEnumerable<BuildingSite> _buildingSites;
        private readonly Hand.OrderCardGroup _playerHand;

        internal CraftsmanAction(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
            _playerHand = player.Hand.OrderCards;
            _buildingSites = player.ConstructionZone;
        }

        public override RoleType Role
        {
            get { return RoleType.Craftsman; }
        }

        protected override MoveSpace GetMoveSpace()
        {
            var moveSpace = new MoveSpace();
            foreach (var handCard in _playerHand)
            {
                PopulateFeedMoves(handCard, moveSpace);
                PopulateBuildMoves(handCard, moveSpace);
            }
            return moveSpace;
        }

        protected override void PopulateFeedMoves(OrderCardModel handCard, MoveSpace moveSpace)
        {
            foreach (BuildingSite buildingSite in _buildingSites)
            {
                if (handCard.GetMaterialType() == buildingSite.MaterialType)
                {
                    IMove<OrderCardModel> feedMove = new Move<OrderCardModel>(handCard, _playerHand,
                        buildingSite.Materials);
                    moveSpace.Add(feedMove);
                }
            }
        }
    }
}