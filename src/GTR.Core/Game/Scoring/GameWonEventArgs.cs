#region

using System.Collections.Generic;
using GTR.Core.Engine;

#endregion

namespace GTR.Core.Game
{
    public delegate void GameWonHandler(object sender, GameWonEventArgs args);

    public class GameWonEventArgs
    {
        public CompletedGame CompletedGame;
    }

    public class CompletedGame
    {
        public IEnumerable<Player> Winners { get; set; }
        public PlayerScores GameScore { get; set; }
    }
}