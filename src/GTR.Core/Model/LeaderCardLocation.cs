#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class LeaderCardLocation : CardSourceTarget<LeaderCardModel>
    {
        public LeaderCardLocation(string player = "") : base(string.Format("Player {0} leader card location", player))
        {
        }
    }
}