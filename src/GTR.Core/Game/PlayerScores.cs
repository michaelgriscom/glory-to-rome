#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace GTR.Core.Game
{
    public class PlayerScores
    {
        private readonly Dictionary<Player, ScoreBreakdown> _playerScores;

        public PlayerScores(IEnumerable<Player> players)
        {
            _playerScores = players.ToDictionary(player => player, player => new ScoreBreakdown());

            Monopolies = new Dictionary<MaterialType, Player>();
        }

        public Dictionary<MaterialType, Player> Monopolies { get; private set; }

        public IEnumerable<Player> Players
        {
            get { return _playerScores.Keys; }
        }

        public ScoreBreakdown GetScoreBreakdown(Player player)
        {
            return _playerScores[player];
        }

        public class ScoreBreakdown
        {
            public int BuildingPoints;
            public int MonopolyPoints;
            public int VaultPoints;

            public ScoreBreakdown()
            {
                BuildingPoints = 0;
                VaultPoints = 0;
                MonopolyPoints = 0;
            }

            public int Total
            {
                get
                {
                    return BuildingPoints + MonopolyPoints +
                           VaultPoints;
                }
            }
        }
    }
}