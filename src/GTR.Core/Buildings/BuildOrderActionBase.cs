#region

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
                MoveCombo buildCombo = GetBuildCombo(handCard);
                if (buildCombo != null)
                {
                    moveSpace.Add(buildCombo);
                }
            }
        }

        private MoveCombo GetBuildCombo(OrderCardModel handCard)
        {
            MoveCombo buildCombo = new MoveCombo();
            var siteDeck = GameTable.GetSiteDeck(handCard.GetMaterialType());
            if (siteDeck.Count == 0)
            {
                return null;
            }
            BuildingSite site = siteDeck.Top;

            // move building card on top of site foundation
            IMove<OrderCardModel> buildAction = new Move<OrderCardModel>(handCard, Player.Hand.OrderCards,
                site.BuildingFoundation);
            buildCombo.Add(buildAction);

            // move site foundation card from deck to player's camp
            IMove<BuildingSite> siteMove = new Move<BuildingSite>(site, siteDeck, Player.ConstructionZone);
            buildCombo.Add(siteMove);

            return buildCombo;
        }

        protected abstract void PopulateFeedMoves(OrderCardModel handCard, MoveSpace moveSpace);
    }
}