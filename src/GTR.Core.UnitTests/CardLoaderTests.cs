#region

using GTR.Core.DeckManagement;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.UnitTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests
{
    [TestClass]
    public class CardLoaderTests
    {
        private ResourceProviderForTest _resourceProvider;

        [TestMethod]
        public void FullCardLoad()
        {
            string cardXml = _resourceProvider.CardXml;
            CardSet cardSet = CardSetSerializer.Deserialize(cardXml);
            Assert.IsTrue(cardSet.Count > 0);
        }

        [TestInitialize]
        public void Initialization()
        {
            _resourceProvider = new ResourceProviderForTest();
        }

        [TestMethod]
        public void SingleBuildingTest()
        {
            string building = "Dock";
            string material = "wood";
            string description = "desc";
            string cardXml = MakeCardXml(building, material, description);
            string fullXml = MakeXml(cardXml);

            CardSet cardSet = CardSetSerializer.Deserialize(fullXml);

            Assert.AreEqual(1, cardSet.Count);
            OrderCardModel card = cardSet.MakeCard(building);
            Assert.AreEqual(building, card.Name);
            Assert.AreEqual(RoleType.Craftsman, card.RoleType);
            Assert.AreEqual(description, card.Description);
        }

        private string MakeCardXml(string building, string material, string description)
        {
            return
                string.Format(
                    "<card><building>{0}</building><material>{1}</material><description>{2}</description></card>",
                    building, material, description);
        }

        private string MakeXml(string cardXml)
        {
            const string xmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?> " +
                                     "<cards xmlns=\"http://tempuri.org/CardsSchema.xsd\" " +
                                     "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                     "xsi:schemaLocation=\"http://tempuri.org/ CardsSchema.xsd\"> ";
            return string.Format("{0}{1}</cards>", xmlHeader, cardXml);
        }
    }
}