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

        internal Hand()
        {
            RefillCapacity = DefaultHandSize;
            JackCards = new JackCardGroup(this);
            OrderCards = new OrderCardGroup(this);
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

        internal class JackCardGroup : CardSourceTarget<JackCardModel>
        {
            private Hand _hand;

            public JackCardGroup(Hand hand)
            {
                _hand = hand;
            }
        }

        internal class OrderCardGroup : BoundedCardTarget<OrderCardModel>, ICardSource<OrderCardModel>
        {
            private readonly Hand _containingHand;

            internal OrderCardGroup(Hand containingHand)
            {
                _containingHand = containingHand;
            }

            public OrderCardModel ElementAt(int index)
            {
                return this[index];
            }

            public override bool CanAdd(OrderCardModel card)
            {
                return _containingHand.Count < _containingHand.RefillCapacity;
            }
        }
    }
}