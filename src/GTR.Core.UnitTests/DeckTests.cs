#region

using System.Collections.Generic;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void ShuffleMultipleCards()
        {
            OrderDeck deck = new OrderDeck();
            HashSet<OrderCardModel> deckCompare = new HashSet<OrderCardModel>();

            int cardCount = 50;
            for (int i = 0; i < cardCount; i++)
            {
                OrderCardModel orderCard = new OrderCardModel("card #" + i, "Test card", RoleType.Architect);
                deck.Add(orderCard);
                deckCompare.Add(orderCard);
            }

            deck.Shuffle();
            Assert.AreEqual(deck.Count, deckCompare.Count);
            while (deck.Count > 0)
            {
                OrderCardModel topCard = deck.Draw();
                Assert.IsTrue(deckCompare.Contains(topCard));
            }
        }
    }
}