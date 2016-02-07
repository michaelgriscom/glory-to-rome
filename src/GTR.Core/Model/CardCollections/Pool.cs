#region

#endregion

namespace GTR.Core.Model.CardCollections
{
    public class Pool : ObservableCardCollection<OrderCardModel>
    {
        public Pool(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public Pool()
        {
        }
    }
}