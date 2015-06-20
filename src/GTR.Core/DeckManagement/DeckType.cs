#region

using System;
using System.Collections.Generic;

#endregion

namespace GTR.Core.DeckManagement
{
    public class DeckType
    {
        private readonly Dictionary<string, int> _cardCounts;

        public DeckType()
        {
            _cardCounts = new Dictionary<string, int>();
        }

        public DeckType(string name)
            : this()
        {
            DeckName = name;
        }

        public DeckType(Dictionary<string, int> cardCounts)
        {
            _cardCounts = cardCounts;
        }

        public int Size { get; private set; }
        public string DeckName { get; set; }

        public void AddCard(string cardName, int cardCount = 1)
        {
            if (cardCount < 1)
            {
                throw new ArgumentOutOfRangeException("cardName");
            }
            if (_cardCounts.ContainsKey(cardName))
            {
                _cardCounts[cardName] += cardCount;
            }
            else
            {
                _cardCounts.Add(cardName, cardCount);
            }
            Size += cardCount;
        }

        public bool Contains(string cardName)
        {
            return _cardCounts.ContainsKey(cardName);
        }

        public ICollection<string> GetCardNames()
        {
            return _cardCounts.Keys;
        }

        public int GetCount(string cardName)
        {
            return _cardCounts[cardName];
        }
    }
}