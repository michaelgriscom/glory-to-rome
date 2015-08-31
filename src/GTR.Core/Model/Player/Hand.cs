#region

using GTR.Core.CardCollections;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model
{
    public class Hand : ObservableModel
    {
        protected const int DefaultHandSize = 5;
        private JackCardGroup _jackCards;
        private OrderCardGroup _orderCards;

        public Hand(string player = "")
        {
            RefillCapacity = DefaultHandSize;
            JackCards = new JackCardGroup(this);
            OrderCards = new OrderCardGroup(this);
        }

        public JackCardGroup JackCards
        {
            get { return _jackCards; }
            set
            {
                _jackCards = value;
                RaisePropertyChanged();
            }
        }

        public OrderCardGroup OrderCards
        {
            get { return _orderCards; }
            set
            {
                _orderCards = value;
                RaisePropertyChanged();
            }
        }

        public int RefillCapacity { get; set; }

        public int Count
        {
            get { return OrderCards.Count + JackCards.Count; }
        }

        public class JackCardGroup : JackDeck
        {
            private readonly Hand _hand;

            public JackCardGroup(Hand hand) : base()
            {
                _hand = hand;
            }
        }

        public class OrderCardGroup : ObservableCardCollection<OrderCardModel>
        {
            private readonly Hand _hand;

            public OrderCardGroup(Hand hand)
            {
                _hand = hand;
            }

            public bool CanAdd(OrderCardModel card)
            {
                return _hand.Count < _hand.RefillCapacity;
            }
        }
    }
}