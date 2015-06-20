#region

using System.Collections.Generic;
using GTR.Core.DeckManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests
{
    [TestClass]
    public class DeckTypeTests
    {
        [TestInitialize]
        public void Initialization()
        {
        }

        [TestMethod]
        public void SingleCard()
        {
            DeckType deckVersion = new DeckType("deckName");
            string cardName = "Dock";
            deckVersion.AddCard(cardName);
            Assert.AreEqual(1, deckVersion.GetCount(cardName));
        }

        [TestMethod]
        public void MultipleOfCard()
        {
            DeckType deckVersion = new DeckType("deckName");

            string cardName = "Palisade";
            int count = 5;
            for (int i = 0; i < count; i++)
            {
                deckVersion.AddCard(cardName);
            }
            Assert.AreEqual(count, deckVersion.GetCount(cardName));
        }

        [TestMethod]
        public void MultipleOfCardOverload()
        {
            DeckType deckVersion = new DeckType("deckName");

            string cardName = "Palisade";
            int count = 5;
            deckVersion.AddCard(cardName, count);
            Assert.AreEqual(count, deckVersion.GetCount(cardName));
        }

        [TestMethod]
        public void SeveralCards()
        {
            string deckName = "deck";
            DeckType deckType = new DeckType(deckName);

            Dictionary<string, int> cards = new Dictionary<string, int>
            {
                {"Palisade", 5},
                {"Gate", 4},
                {"Bridge", 3},
                {"Insula", 2}
            };

            int totalCards = 0;
            foreach (var entry in cards)
            {
                deckType.AddCard(entry.Key, entry.Value);
                totalCards += entry.Value;
            }
            Assert.AreEqual(totalCards, deckType.Size, "Total card count should be accurate");

            foreach (var entry in cards)
            {
                int expectedCount = entry.Value;
                int actualCount = deckType.GetCount(entry.Key);
                Assert.AreEqual(expectedCount, actualCount, "Each card count should be accurate");
            }
        }

        [TestMethod]
        public void DeckName()
        {
            string deckName = "deckName123445";
            DeckType deckVersion = new DeckType(deckName);

            Assert.AreEqual(deckName, deckVersion.DeckName);
        }
    }
}