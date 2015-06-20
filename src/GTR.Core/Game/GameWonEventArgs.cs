#region

using System.Collections.Generic;

#endregion

namespace GTR.Core.Game
{
    public delegate void GameWonHandler(object sender, GameWonEventArgs args);

    public class GameWonEventArgs
    {
        public IEnumerable<Player> Winners { get; set; }
        public PlayerScores GameScore { get; set; }
    }
}