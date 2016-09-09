#region

using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class DemandArea : ObservableCardCollection<OrderCardModel>
    {
        public DemandArea(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public DemandArea()
        {
        }
    }
}