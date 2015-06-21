#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class DemandArea : CardSourceTarget<OrderCardModel>
    {
        public DemandArea(string player = "")
            : base(string.Format("Player {0} demand area", player))
        {
        }
    }
}