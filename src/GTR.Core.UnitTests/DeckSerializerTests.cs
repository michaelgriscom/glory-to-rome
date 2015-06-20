#region

using System.Collections.Generic;
using GTR.Core.DeckManagement;
using GTR.Core.Services;
using GTR.Core.UnitTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests
{
    [TestClass]
    public class DeckSerializerTests
    {
        private IDeckIo _deckIo;

        [TestMethod]
        public void RepublicDeckDeserialization()
        {
            string republicDeck = _deckIo.RepublicDeckSerialization;
            DeckType deckVersion = DeckTypeSerializer.Deserialize(republicDeck);
            Assert.IsTrue(deckVersion.Size > 0);
        }

        [TestMethod]
        public void RepublicDeckSerializationConsistency()
        {
            string republicDeck = _deckIo.RepublicDeckSerialization;
            DeckType deckVersion = DeckTypeSerializer.Deserialize(republicDeck);
            CheckSerializationConsistency(deckVersion);
        }

        [TestMethod]
        public void ImperiumDeckDeserialization()
        {
            string imperiumDeck = _deckIo.ImperiumDeckSerialization;
            DeckType deckVersion = DeckTypeSerializer.Deserialize(imperiumDeck);
            Assert.IsTrue(deckVersion.Size > 0);
        }

        [TestMethod]
        public void ImperiumDeckSerializationConsistency()
        {
            string imperiumDeck = _deckIo.ImperiumDeckSerialization;
            DeckType deckVersion = DeckTypeSerializer.Deserialize(imperiumDeck);
            CheckSerializationConsistency(deckVersion);
        }

        [TestInitialize]
        public void Initialization()
        {
            _deckIo = new DeckIoForTest();
        }

        [TestMethod]
        public void OneOfCardSerializationConsistency()
        {
            string deckname = "deck";
            DeckType originalDeck = new DeckType(deckname);

            string building = "Dock";
            originalDeck.AddCard(building);

            CheckSerializationConsistency(originalDeck);
        }

        private void CheckSerializationConsistency(DeckType originalDeck)
        {
            string deckCsv = DeckTypeSerializer.Serialize(originalDeck);
            DeckType deserializedVersion = DeckTypeSerializer.Deserialize(deckCsv);
            CheckDeepEqual(originalDeck, deserializedVersion);
        }

        private void CheckDeepEqual(DeckType expectedDeck, DeckType actualDeck)
        {
            Assert.AreEqual(expectedDeck.DeckName, actualDeck.DeckName, "Deck names must be equal");
            var expectedCardNames = expectedDeck.GetCardNames();
            var actualCardNames = actualDeck.GetCardNames();
            Assert.AreEqual(expectedCardNames.Count, actualCardNames.Count, "Unique card counts must be equal");

            foreach (string cardName in expectedCardNames)
            {
                int expectedCount = expectedDeck.GetCount(cardName);
                int actualCount = actualDeck.GetCount(cardName);

                Assert.AreEqual(expectedCount, actualCount, "Count of each card must be equal");
            }
        }

        [TestMethod]
        public void MultipleOfCardSerializationConsistency()
        {
            string deckname = "deck123";
            DeckType deckType = new DeckType(deckname);

            string building = "Palisade";
            int count = 5;
            deckType.AddCard(building, count);

            CheckSerializationConsistency(deckType);
        }

        [TestMethod]
        public void SeveralCardsSerializationConsistency()
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

            foreach (var entry in cards)
            {
                deckType.AddCard(entry.Key, entry.Value);
            }

            CheckSerializationConsistency(deckType);
        }
    }
}