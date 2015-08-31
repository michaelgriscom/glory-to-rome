using GTR.Core.Model.CardCollections;

namespace GTR.Core.Model
{
    public class OrderDeck : Deck<OrderCardModel>
    {
        public OrderDeck(ICardCollection<OrderCardModel> cl) : base(cl)
        {
        }

        public OrderDeck()
        {
            
        }
    }
}