#region

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

        public bool Perform(MoveMaker moveMaker)
        {
          return  moveMaker.MakeMove(BuildMove.Card, BuildMove.Source, BuildMove.Destination, null) &&
            moveMaker.MakeMove(SiteMove.Card, SiteMove.Source, SiteMove.Destination, null);

      
        }
    }
}