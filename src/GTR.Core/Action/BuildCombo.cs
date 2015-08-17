#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Action
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

        public IEnumerator<IMove<CardModelBase>> GetEnumerator()
        {
            yield return BuildMove;
            yield return SiteMove;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}