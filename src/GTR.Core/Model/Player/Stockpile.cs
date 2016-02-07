#region

using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Stockpile : ObservableCardCollection<OrderCardModel>
    {
        public Stockpile(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public Stockpile()
        {
        }

    }
}