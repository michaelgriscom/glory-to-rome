#region

#endregion

#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class CompletedFoundations : CardTarget<BuildingSite>
    {
        public CompletedFoundations(string player = "")
            : base(string.Format("Player {0} completed foundations", player))
        {
        }
    }
}