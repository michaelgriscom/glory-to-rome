namespace GTR.Core.Model.CardCollections
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