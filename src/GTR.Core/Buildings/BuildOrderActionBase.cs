#region

using GTR.Core.Action;
using GTR.Core.Game;
using GTR.Core.ManipulatableRules.Actions;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Buildings
{
    internal abstract class BuildOrderActionBase : OrderActionBase
    {
        public BuildOrderActionBase(Player player, GameTable gameTable)
            : base(player, gameTable)
        {
        }

        public abstract RoleType Role { get; }

        protected virtual void PopulateBuildMoves(OrderCardModel handCard, MoveSpace moveSpace)
        {
            int buildCost = GameTable.GetSiteCost(handCard.GetMaterialType());
            int outstandingActions = Player.GetActions(Role);

            if (outstandingActions >= buildCost)
            {
                var buildCombo = GetBuildCombo(handCard);
                if (buildCombo != null)
                {
                    moveSpace.Add(buildCombo);
                }
            }
        }

        private BuildCombo GetBuildCombo(OrderCardModel handCard)
        {
            var siteDeck = GameTable.GetSiteDeck(handCard.GetMaterialType());
            if (siteDeck.Count == 0)
            {
                return null;
            }
            BuildingSite site = siteDeck.Top;

            // move building card on top of site foundation
            IMove<OrderCardModel> buildAction = new Move<OrderCardModel>(handCard, Player.Board.Hand.OrderCards,
                site.BuildingFoundation);

            // move site foundation card from deck to player's camp
            IMove<BuildingSite> siteMove = new Move<BuildingSite>(site, siteDeck, Player.Board.ConstructionZone);

            BuildCombo buildCombo = new BuildCombo(siteMove, buildAction);
            return buildCombo;
        }

        protected abstract void PopulateFeedMoves(OrderCardModel handCard, MoveSpace moveSpace);
    }
}