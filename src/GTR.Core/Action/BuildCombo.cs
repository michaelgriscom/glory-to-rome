#region

using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Action
{
    public class BuildCombo : IAction
    {
        public IMove<OrderCardModel> BuildMove { get; private set; }

        public IMove<BuildingSite> SiteMove { get; private set; }

        public BuildCombo(IMove<BuildingSite> siteMove, IMove<OrderCardModel> buildMove)
        {
            this.BuildMove = buildMove;
            this.SiteMove = siteMove;
        }

        public bool Perform()
        {
            return BuildMove.Perform() && SiteMove.Perform();
        }
    }
}