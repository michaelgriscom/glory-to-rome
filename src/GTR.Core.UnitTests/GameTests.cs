#region

using GTR.Core.Game;
using GTR.Core.Services;
using GTR.Core.UnitTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void DefaultGame()
        {
            // placeholder for debugging until the UI is further along
            GameOptions options = new GameOptions("Republic");
            IDeckIo deckIo = new DeckIoForTest();
            IResourceProvider rp = new ResourceProviderForTest();
            IMessageProvider mp = new InMemoryMessageProvider();
            Game.Game game = new Game.Game(2, options, deckIo, rp, mp);
            try
            {
                game.PlayGame();
            }
            catch
            {
                // don't care, this test is just to help debug
            }
        }
    }
}