#region

using System.Collections.Generic;
using System.Linq;
using GTR.Core.Engine;

#endregion

namespace GTR.Core.Game
{
    public static class GameScorer
    {
        private const int MonopolyBonus = 3;

        public static PlayerScores Score(IEnumerable<Player> players)
        {
            PlayerScores gameScore = new PlayerScores(players);

            TallyBuildings(gameScore);
            TallyVaults(gameScore);
            TallyMonopolies(gameScore);

            return gameScore;
        }

        private static void TallyMonopolies(PlayerScores gameScore)
        {
            foreach (var monopoly in gameScore.Monopolies)
            {
                gameScore.GetScoreBreakdown(monopoly.Value).MonopolyPoints += MonopolyBonus;
            }
        }

        private static void TallyBuildings(PlayerScores gameScore)
        {
            foreach (var player in gameScore.Players)
            {
                gameScore.GetScoreBreakdown(player).BuildingPoints = player.Camp.InfluencePoints;
            }
        }

        private static void TallyVaults(PlayerScores gameScore)
        {
            var runningMonopolyScore = EnumExtensions.GetEnumList<MaterialType>()
                .ToDictionary(material => material, material => 0);
            foreach (Player player in gameScore.Players)
            {
                var playerScore = gameScore.GetScoreBreakdown(player);

                var vault = player.Camp.Vault;

                var groupedMaterials = from card in vault
                    group card by card.RoleType
                    into g
                    select new {MaterialType = g.Key.ToMaterial(), Count = g.Count()};

                foreach (var materialGroup in groupedMaterials)
                {
                    int materialWorth = materialGroup.MaterialType.MaterialWorth();
                    int materialCount = materialGroup.Count;
                    var materialType = materialGroup.MaterialType;

                    playerScore.VaultPoints += materialWorth*materialCount;

                    if (materialCount > runningMonopolyScore[materialType])
                    {
                        runningMonopolyScore[materialType] = materialCount;
                        gameScore.Monopolies.Remove(materialType);
                        gameScore.Monopolies.Add(materialType, player);
                    }
                    else if (materialCount == runningMonopolyScore[materialType])
                    {
                        gameScore.Monopolies.Remove(materialType);
                    }
                }
            }
        }

        public static List<Player> CalculateWinners(PlayerScores gameScore)
        {
            var totalScores = gameScore.Players.ToDictionary(player => player,
                player => gameScore.GetScoreBreakdown(player).Total);

            int maxScore = totalScores.Max(playerEntry => playerEntry.Value);
            var topPlayers = totalScores.Where(playerEntry => playerEntry.Value == maxScore);

            int maxHandSize = 0;
            var winners = new List<Player>();
            foreach (var player in topPlayers.Select(playerEntry => playerEntry.Key))
            {
                if (player.Hand.Count > maxHandSize)
                {
                    maxHandSize = player.Hand.Count;
                    winners = new List<Player> {player};
                }
                else if (player.Hand.Count == maxHandSize)
                {
                    winners.Add(player);
                }
            }
            return winners;
        }
    }
}