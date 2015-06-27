#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Hand
    {
        protected const int DefaultHandSize = 5;

        internal Hand(string player = "")
        {
            RefillCapacity = DefaultHandSize;
            string locationName = string.Format("Player {0} hand", player);
            JackCards = new JackCardGroup(this, locationName);
            OrderCards = new OrderCardGroup(this, locationName);
        }

        internal JackCardGroup JackCards { get; private set; }
        internal OrderCardGroup OrderCards { get; private set; }
        internal int RefillCapacity { get; set; }

        public int Count
        {
            get { return OrderCards.Count + JackCards.Count; }
        }

        internal class JackCardGroup : CardSourceTarget<JackCardModel>
        {
            private readonly Hand _hand;

            public JackCardGroup(Hand hand, string locationName) : base(locationName) 
            {
                _hand = hand;
            }
        }

        internal class OrderCardGroup : BoundedCardTarget<OrderCardModel>, ICardSource<OrderCardModel>
        {
            private readonly Hand _hand;

            internal OrderCardGroup(Hand hand, string locationName)
                : base(locationName)
            {
                _hand = hand;
            }

            public OrderCardModel ElementAt(int index)
            {
                return this[index];
            }

            public override bool CanAdd(OrderCardModel card)
            {
                return _hand.Count < _hand.RefillCapacity;
            }
        }
    }
}