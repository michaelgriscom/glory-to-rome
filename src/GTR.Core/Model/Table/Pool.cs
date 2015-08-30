#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Pool : ObservableCardCollection<OrderCardModel>
    {
        public Pool() : base()
        {
        }
    }
}