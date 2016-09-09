#region

using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class CompletedBuildings : ObservableCardCollection<OrderCardModel>
    {
        public CompletedBuildings(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public CompletedBuildings()
        {
        }

    }
}