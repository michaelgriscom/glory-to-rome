#region

using System.Collections.Generic;
using System.Threading.Tasks;
using GTR.Core.AIController;
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
        public async Task DefaultGame()
        {
            // placeholder for debugging until the UI is further along
            GameOptions options = new GameOptions("Republic", 2);
            DeckIoForTest deckIo = new DeckIoForTest();
            ResourceProviderForTest rp = new ResourceProviderForTest();
            InMemoryMessageProvider mp = new InMemoryMessageProvider();
            var playerInputs = new Dictionary<string, IPlayerInput>();
            playerInputs.Add("player1", new AiPlayerInput());
            playerInputs.Add("player2", new AiPlayerInput());

            Model.Game game = new Model.Game(playerInputs, options, deckIo, rp, mp);
            try
            {
                await game.PlayGame();
            }
            catch
            {
                // don't care, this test is just to help debug
            }
        }
    }
}