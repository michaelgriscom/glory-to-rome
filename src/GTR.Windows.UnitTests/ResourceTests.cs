#region

using GTR.Core.DeckManagement;
using GTR.Windows.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Windows.UnitTests
{
    [TestClass]
    public class ResourceTests
    {
        private WpfResourceProvider _fileProvider;

        [TestMethod]
        public void CardLoaderResource()
        {
            string cardXml = _fileProvider.CardXml;

            CardSet cardSet = CardSetSerializer.Deserialize(cardXml);
            Assert.IsTrue(cardSet.Count > 0);
        }

        [TestInitialize]
        public void Initialization()
        {
            _fileProvider = new WpfResourceProvider();
        }
    }
}