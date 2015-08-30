#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.DeckManagement
{
    public class CardSet : IEnumerable<OrderCardModel>
    {
        private readonly Dictionary<string, OrderCardModel> _cards;

        public CardSet()
        {
            _cards = new Dictionary<string, OrderCardModel>();
        }

        public int Count
        {
            get { return _cards.Count; }
        }

        public IEnumerator<OrderCardModel> GetEnumerator()
        {
            return _cards.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public OrderCardModel MakeCard(string cardName)
        {
            var card = _cards[cardName];
            return new OrderCardModel(card.Name, card.Description, card.RoleType);
        }

        internal void Add(OrderCardModel card)
        {
            _cards.Add(card.Name, card);
        }
    }
}