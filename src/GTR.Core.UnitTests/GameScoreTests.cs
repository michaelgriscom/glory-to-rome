#region

using System.Collections.Generic;
using System.Linq;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests
{
    [TestClass]
    public class GameScoreTests
    {
        // every player starts with score of 2
        private const int BaseScore = 2;
        private GameTable _gameTable;

        [TestInitialize]
        public void Initialization()
        {
            Deck<OrderCardModel> orderDeck = new Deck<OrderCardModel>();
            CardSourceTarget<JackCardModel> jackDeck = new CardSourceTarget<JackCardModel>();

            _gameTable = new GameTable(orderDeck, jackDeck);
        }

        [TestMethod]
        public void ScorelessPlayer()
        {
            Player player = new Player("test-player", null);
            List<Player> players = new List<Player>
            {
                player
            };
            _gameTable.AddPlayers(players);

            var gameScore = GameScorer.Score(players);
            int playerScore = gameScore.GetScoreBreakdown(player).Total;

            Assert.AreEqual(BaseScore, playerScore);
        }

        [TestMethod]
        public void PlayerWithBuildings()
        {
            Player player = new Player("test-player", null);
            var players = new List<Player>
            {
                player
            };
            _gameTable.AddPlayers(players);

            var brickFoundation = new BuildingSite(MaterialType.Brick);
            var concreteFoundation = new BuildingSite(MaterialType.Concrete);
            var marbleFoundation = new BuildingSite(MaterialType.Marble);
            var stoneFoundation = new BuildingSite(MaterialType.Stone);
            var woodFoundation = new BuildingSite(MaterialType.Wood);
            var rubbleFoundation = new BuildingSite(MaterialType.Rubble);

            player.Board.Camp.CompletedFoundations.Add(brickFoundation);
            player.Board.Camp.CompletedFoundations.Add(concreteFoundation);
            player.Board.Camp.CompletedFoundations.Add(marbleFoundation);
            player.Board.Camp.CompletedFoundations.Add(stoneFoundation);
            player.Board.Camp.CompletedFoundations.Add(woodFoundation);
            player.Board.Camp.CompletedFoundations.Add(rubbleFoundation);

            var gameScore = GameScorer.Score(players);
            int playerScore = gameScore.GetScoreBreakdown(player).Total;

            int expectedPlayerScore = BaseScore + MaterialType.Concrete.MaterialWorth() +
                                      MaterialType.Brick.MaterialWorth() +
                                      MaterialType.Marble.MaterialWorth() + MaterialType.Rubble.MaterialWorth() +
                                      MaterialType.Stone.MaterialWorth() + MaterialType.Wood.MaterialWorth();
            Assert.AreEqual(expectedPlayerScore, playerScore);
        }

        [TestMethod]
        public void PlayersWithBuildingsAndVaults()
        {
            Player player1 = new Player("test-player1", null);
            Player player2 = new Player("test-player2", null);

            var players = new List<Player>
            {
                player1,
                player2
            };
            _gameTable.AddPlayers(players);

            var brickFoundation1 = new BuildingSite(MaterialType.Brick);
            var brickFoundation2 = new BuildingSite(MaterialType.Brick);
            var concreteFoundation = new BuildingSite(MaterialType.Concrete);
            var stoneFoundation = new BuildingSite(MaterialType.Stone);

            player1.Board.Camp.CompletedFoundations.Add(brickFoundation1);
            player1.Board.Camp.CompletedFoundations.Add(brickFoundation2);
            player1.Board.Camp.CompletedFoundations.Add(concreteFoundation);
            player1.Board.Camp.CompletedFoundations.Add(stoneFoundation);


            int player1BuildingScore = brickFoundation1.MaterialType.MaterialWorth()*2 +
                                       concreteFoundation.MaterialType.MaterialWorth() +
                                       stoneFoundation.MaterialType.MaterialWorth();

            var player1BrickMaterial = new OrderCardModel("p1 vault material", null, RoleType.Legionnaire);
            var player1StoneMaterial1 = new OrderCardModel("p1 vault material", null, RoleType.Merchant);
            var player1StoneMaterial2 = new OrderCardModel("p1 vault material", null, RoleType.Merchant);
            var player1MarbleMaterial1 = new OrderCardModel("p1 vault material", null, RoleType.Patron);
            var player1MarbleMaterial2 = new OrderCardModel("p1 vault material", null, RoleType.Patron);
            var player1MarbleMaterial3 = new OrderCardModel("p1 vault material", null, RoleType.Patron);

            player1.Board.Camp.Vault.Add(player1BrickMaterial);
            player1.Board.Camp.Vault.Add(player1StoneMaterial1);
            player1.Board.Camp.Vault.Add(player1StoneMaterial2);
            player1.Board.Camp.Vault.Add(player1MarbleMaterial1);
            player1.Board.Camp.Vault.Add(player1MarbleMaterial2);
            player1.Board.Camp.Vault.Add(player1MarbleMaterial3);
            int player1Monopolies = 2;
            int player1MaterialScores = player1BrickMaterial.GetMaterialType().MaterialWorth() +
                                        player1StoneMaterial1.GetMaterialType().MaterialWorth()*2 +
                                        player1MarbleMaterial1.GetMaterialType().MaterialWorth()*3;
            int player1ExpectedScore = BaseScore + player1BuildingScore + player1Monopolies*3 + player1MaterialScores;


            var rubbleFoundation = new BuildingSite(MaterialType.Rubble);
            player2.Board.Camp.CompletedFoundations.Add(rubbleFoundation);

            int player2BuildingScore = rubbleFoundation.MaterialType.MaterialWorth();

            var player2RubbleMaterial1 = new OrderCardModel("p2 vault material", null, RoleType.Laborer);
            var player2RubbleMaterial2 = new OrderCardModel("p2 vault material", null, RoleType.Laborer);
            var player2StoneMaterial1 = new OrderCardModel("p2 vault material", null, RoleType.Merchant);
            var player2StoneMaterial2 = new OrderCardModel("p2 vault material", null, RoleType.Merchant);
            var player2WoodMaterial = new OrderCardModel("p2 vault material", null, RoleType.Craftsman);
            var player2ConcreteMaterial = new OrderCardModel("p2 vault material", null, RoleType.Architect);
            player2.Board.Camp.Vault.Add(player2RubbleMaterial1);
            player2.Board.Camp.Vault.Add(player2RubbleMaterial2);
            player2.Board.Camp.Vault.Add(player2StoneMaterial1);
            player2.Board.Camp.Vault.Add(player2StoneMaterial2);
            player2.Board.Camp.Vault.Add(player2WoodMaterial);
            player2.Board.Camp.Vault.Add(player2ConcreteMaterial);
            int player2Monopolies = 3;
            int player2MaterialScores = player2RubbleMaterial1.GetMaterialType().MaterialWorth()*2 +
                                        player2WoodMaterial.GetMaterialType().MaterialWorth() +
                                        player2StoneMaterial1.GetMaterialType().MaterialWorth()*2 +
                                        player2ConcreteMaterial.GetMaterialType().MaterialWorth();
            int player2ExpectedScore = BaseScore + player2BuildingScore + player2Monopolies*3 + player2MaterialScores;


            var gameScore = GameScorer.Score(players);

            int player1Score = gameScore.GetScoreBreakdown(player1).Total;
            Assert.AreEqual(player1ExpectedScore, player1Score);

            int player2Score = gameScore.GetScoreBreakdown(player2).Total;
            Assert.AreEqual(player2ExpectedScore, player2Score);
        }

        [TestMethod]
        public void UncontestedMonopolies()
        {
            Player player1 = new Player("test-player1", null);
            Player player2 = new Player("test-player2", null);

            List<Player> players = new List<Player>
            {
                player1,
                player2
            };
            _gameTable.AddPlayers(players);

            var player1BrickMaterial = new OrderCardModel("p1 vault material", null, RoleType.Legionnaire);
            var player1StoneMaterial = new OrderCardModel("p1 vault material", null, RoleType.Merchant);
            var player1MarbleMaterial = new OrderCardModel("p1 vault material", null, RoleType.Patron);
            player1.Board.Camp.Vault.Add(player1BrickMaterial);
            player1.Board.Camp.Vault.Add(player1StoneMaterial);
            player1.Board.Camp.Vault.Add(player1MarbleMaterial);
            int player1Monopolies = 3;
            int player1MaterialScores = player1BrickMaterial.GetMaterialType().MaterialWorth() +
                                        player1StoneMaterial.GetMaterialType().MaterialWorth() +
                                        player1MarbleMaterial.GetMaterialType().MaterialWorth();
            int player1ExpectedScore = BaseScore + player1Monopolies*3 + player1MaterialScores;


            var player2RubbleMaterial = new OrderCardModel("p2 vault material", null, RoleType.Laborer);
            var player2WoodMaterial = new OrderCardModel("p2 vault material", null, RoleType.Craftsman);
            var player2ConcreteMaterial = new OrderCardModel("p2 vault material", null, RoleType.Architect);
            player2.Board.Camp.Vault.Add(player2RubbleMaterial);
            player2.Board.Camp.Vault.Add(player2WoodMaterial);
            player2.Board.Camp.Vault.Add(player2ConcreteMaterial);
            int player2Monopolies = 3;
            int player2MaterialScores = player2RubbleMaterial.GetMaterialType().MaterialWorth() +
                                        player2WoodMaterial.GetMaterialType().MaterialWorth() +
                                        player2ConcreteMaterial.GetMaterialType().MaterialWorth();
            int player2ExpectedScore = BaseScore + player2Monopolies*3 + player2MaterialScores;


            var gameScore = GameScorer.Score(players);

            int player1Score = gameScore.GetScoreBreakdown(player1).Total;
            Assert.AreEqual(player1ExpectedScore, player1Score);

            int player2Score = gameScore.GetScoreBreakdown(player2).Total;
            Assert.AreEqual(player2ExpectedScore, player2Score);
        }

        [TestMethod]
        public void ContestedMonopolies()
        {
            Player player1 = new Player("test-player1", null);
            Player player2 = new Player("test-player2", null);

            var players = new List<Player>
            {
                player1,
                player2
            };
            _gameTable.AddPlayers(players);

            var player1BrickMaterial = new OrderCardModel("p1 vault material", null, RoleType.Legionnaire);
            var player1StoneMaterial = new OrderCardModel("p1 vault material", null, RoleType.Merchant);
            var player1MarbleMaterial = new OrderCardModel("p1 vault material", null, RoleType.Patron);
            player1.Board.Camp.Vault.Add(player1BrickMaterial);
            player1.Board.Camp.Vault.Add(player1StoneMaterial);
            player1.Board.Camp.Vault.Add(player1MarbleMaterial);
            int player1MaterialScores = player1BrickMaterial.GetMaterialType().MaterialWorth() +
                                        player1StoneMaterial.GetMaterialType().MaterialWorth() +
                                        player1MarbleMaterial.GetMaterialType().MaterialWorth();
            int player1ExpectedScore = BaseScore + player1MaterialScores;


            var player2BrickMaterial = new OrderCardModel("p2 vault material", null, RoleType.Legionnaire);
            var player2StoneMaterial = new OrderCardModel("p2 vault material", null, RoleType.Merchant);
            var player2MarbleMaterial = new OrderCardModel("p2 vault material", null, RoleType.Patron);
            player2.Board.Camp.Vault.Add(player2BrickMaterial);
            player2.Board.Camp.Vault.Add(player2StoneMaterial);
            player2.Board.Camp.Vault.Add(player2MarbleMaterial);
            int player2MaterialScores = player2BrickMaterial.GetMaterialType().MaterialWorth() +
                                        player2StoneMaterial.GetMaterialType().MaterialWorth() +
                                        player2MarbleMaterial.GetMaterialType().MaterialWorth();
            int player2ExpectedScore = BaseScore + player2MaterialScores;


            var gameScore = GameScorer.Score(players);

            int player1Score = gameScore.GetScoreBreakdown(player1).Total;
            Assert.AreEqual(player1ExpectedScore, player1Score);

            int player2Score = gameScore.GetScoreBreakdown(player2).Total;
            Assert.AreEqual(player2ExpectedScore, player2Score);
        }

        [TestMethod]
        public void ClearVictory()
        {
            Player player1 = new Player("test-player1", null);
            Player player2 = new Player("test-player2", null);
            Player player3 = new Player("test-player3", null);
            Player player4 = new Player("test-player4", null);

            var players = new List<Player>
            {
                player1,
                player2,
                player3,
                player4
            };
            _gameTable.AddPlayers(players);

            var player3BrickMaterial = new OrderCardModel(null, null, RoleType.Legionnaire);
            player3.Board.Camp.Vault.Add(player3BrickMaterial);

            var gameScore = GameScorer.Score(players);
            var winners = GameScorer.CalculateWinners(gameScore);

            Assert.AreEqual(1, winners.Count);
            Assert.AreEqual(player3, winners.First());
        }

        [TestMethod]
        public void HandSizeVictory()
        {
            Player player1 = new Player("test-player1", null);
            Player player2 = new Player("test-player2", null);
            Player player3 = new Player("test-player3", null);
            Player player4 = new Player("test-player4", null);

            var players = new List<Player>
            {
                player1,
                player2,
                player3,
                player4
            };
            _gameTable.AddPlayers(players);

            var player4BrickMaterial = new OrderCardModel(null, null, RoleType.Legionnaire);
            player4.Board.Camp.Vault.Add(player4BrickMaterial);

            var player1BrickMaterial = new OrderCardModel(null, null, RoleType.Legionnaire);
            player1.Board.Camp.Vault.Add(player1BrickMaterial);

            int player1HandSize = 5;
            for (int i = 0; i < player1HandSize; i++)
            {
                var player1HandCard = new OrderCardModel(null, null, RoleType.Architect);
                player1.Board.Hand.OrderCards.Add(player1HandCard);
            }

            int player4HandSize = 4;
            for (int i = 0; i < player4HandSize; i++)
            {
                var player4HandCard = new OrderCardModel(null, null, RoleType.Craftsman);
                player4.Board.Hand.OrderCards.Add(player4HandCard);
            }

            var gameScore = GameScorer.Score(players);
            var winners = GameScorer.CalculateWinners(gameScore);

            Assert.AreEqual(1, winners.Count);
            Assert.AreEqual(player1, winners.First());
        }

        [TestMethod]
        public void TieGame()
        {
            Player player1 = new Player("test-player1", null);
            Player player2 = new Player("test-player2", null);
            Player player3 = new Player("test-player3", null);
            Player player4 = new Player("test-player4", null);

            var players = new List<Player>
            {
                player1,
                player2,
                player3,
                player4
            };
            _gameTable.AddPlayers(players);

            var player2BrickMaterial = new OrderCardModel(null, null, RoleType.Legionnaire);
            player2.Board.Camp.Vault.Add(player2BrickMaterial);

            var player3BrickMaterial = new OrderCardModel(null, null, RoleType.Legionnaire);
            player3.Board.Camp.Vault.Add(player3BrickMaterial);

            int player2HandSize = 3;
            for (int i = 0; i < player2HandSize; i++)
            {
                var player2HandCard = new OrderCardModel(null, null, RoleType.Architect);
                player2.Board.Hand.OrderCards.Add(player2HandCard);
            }

            int player3HandSize = 3;
            for (int i = 0; i < player3HandSize; i++)
            {
                var player3HandCard = new OrderCardModel(null, null, RoleType.Craftsman);
                player3.Board.Hand.OrderCards.Add(player3HandCard);
            }

            var gameScore = GameScorer.Score(players);
            var winners = GameScorer.CalculateWinners(gameScore);

            Assert.AreEqual(2, winners.Count);
            Assert.IsTrue(winners.Contains(player2));
            Assert.IsTrue(winners.Contains(player3));
        }
    }
}