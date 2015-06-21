#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    internal class Hand : ICardSource<HandCardModel>
    {
        protected const int DefaultHandSize = 5;

        internal Hand(string player = "")
        {
            RefillCapacity = DefaultHandSize;
            JackCards = new JackCardGroup(this);
            OrderCards = new OrderCardGroup(this);
            LocationName = string.Format("Player {0} hand", player);
        }

        internal JackCardGroup JackCards { get; private set; }
        internal OrderCardGroup OrderCards { get; private set; }
        internal int RefillCapacity { get; set; }

        public IEnumerator<HandCardModel> GetEnumerator()
        {
            foreach (OrderCardModel card in OrderCards)
            {
                yield return card;
            }
            foreach (JackCardModel card in JackCards)
            {
                yield return card;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return OrderCards.Count + JackCards.Count; }
        }

        public HandCardModel ElementAt(int index)
        {
            if (index < OrderCards.Count)
            {
                return OrderCards.ElementAt(index);
            }
            int jackIndex = index - OrderCards.Count;
            return JackCards.ElementAt(jackIndex);
        }

        public void RemoveAt(int index)
        {
            if (index < OrderCards.Count)
            {
                OrderCards.RemoveAt(index);
            }
            else
            {
                int jackIndex = index - OrderCards.Count;
                JackCards.RemoveAt(jackIndex);
            }
        }

        public string LocationName { get; private set; }

        internal class JackCardGroup : CardSourceTarget<JackCardModel>
        {
            private readonly Hand _hand;

            public JackCardGroup(Hand hand)
            {
                _hand = hand;
            }

            public override string LocationName
            {
                get { return _hand.LocationName; }
            }
        }

        internal class OrderCardGroup : BoundedCardTarget<OrderCardModel>, ICardSource<OrderCardModel>
        {
            private readonly Hand _hand;

            internal OrderCardGroup(Hand hand)
            {
                _hand = hand;
            }

            public OrderCardModel ElementAt(int index)
            {
                return this[index];
            }

            public override string LocationName
            {
                get { return _hand.LocationName; }
            }

            public override bool CanAdd(OrderCardModel card)
            {
                return _hand.Count < _hand.RefillCapacity;
            }
        }
    }
}