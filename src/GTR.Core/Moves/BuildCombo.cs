#region

using GTR.Core.Action;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Moves
{
    public class BuildCombo : IAction
    {
        public BuildCombo(IMove<BuildingSite> siteMove, IMove<OrderCardModel> buildMove)
        {
            BuildMove = buildMove;
            SiteMove = siteMove;
        }

        public IMove<OrderCardModel> BuildMove { get; }
        public IMove<BuildingSite> SiteMove { get; }

        public bool Perform()
        {
            return BuildMove.Perform() && SiteMove.Perform();
        }
    }
}