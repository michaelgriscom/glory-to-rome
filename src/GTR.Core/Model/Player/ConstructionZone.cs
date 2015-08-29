#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class ConstructionZone : CardSourceTarget<BuildingSite>
    {
        public ConstructionZone(string player = "") : base(string.Format("Player {0} construction zone", player))
        {
        }
    }
}