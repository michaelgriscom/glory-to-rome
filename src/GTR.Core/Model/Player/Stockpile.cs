#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Stockpile : ObservableCardCollection<OrderCardModel>
    {
        public Stockpile() : base()
        {
        }
    }
}