#region

using System.Collections.Generic;
using System.Linq;
using GTR.Core.Actions;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Moves;
using GTR.Core.UnitTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests.Actions
{
    [TestClass]
    public class ArchitectTests
    {
        private ArchitectAction _action;
        private GameTable _gameTable;
        private Player _player;

        [TestInitialize]
        public void Initialization()
        {
            OrderDeck orderDeck = new OrderDeck();
            JackDeck jackDeck = new JackDeck();

            _gameTable = new GameTable() { OrderDeck = orderDeck, JackDeck = jackDeck };

            PlayerInputForTest input = new PlayerInputForTest();

            _player = new Player("test-player");
            List<Player> players = new List<Player> {_player}
                ;
            _gameTable.AddPlayers(players);
            _action = new ArchitectAction(_player, _gameTable);
        }

        [TestMethod]
        public void BuildArchitect()
        {
            RoleType handCardRole = RoleType.Craftsman;
            MaterialType handCardMaterial = handCardRole.ToMaterial();

            OrderCardModel handCard = new OrderCardModel("hand card", "test", handCardRole);
            _player.Hand.OrderCards.Add(handCard);
            _player.OutstandingActions.Add(RoleType.Architect);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            enumerator.MoveNext();

            // there should be two moves, to move the hand card to the build site and to move the build site to the construction zone
            var moveCombo = enumerator.Current;
            Assert.IsInstanceOfType(moveCombo, typeof (BuildCombo));
            var buildCombo = moveCombo as BuildCombo;

            // ReSharper disable once PossibleNullReferenceException
            var buildAction = buildCombo.BuildMove;

            Assert.AreEqual(buildAction.Card, handCard);
            Assert.AreEqual(buildAction.Source, _player.Hand.OrderCards);
            Assert.AreEqual(buildAction.Destination, _gameTable.GetSiteDeck(handCardMaterial).Top.BuildingFoundation);
            var siteMove = buildCombo.SiteMove;
            Assert.AreEqual(siteMove.Card, _gameTable.GetSiteDeck(handCardMaterial).Top);
            Assert.AreEqual(siteMove.Source, _gameTable.GetSiteDeck(handCardMaterial));
            Assert.AreEqual(siteMove.Destination, _player.ConstructionZone);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void FeedArchitect()
        {
            RoleType stockpileCardRole = RoleType.Merchant;
            MaterialType stockpileCardMaterial = stockpileCardRole.ToMaterial();

            OrderCardModel stockpileCard = new OrderCardModel("stockpile card", "test", stockpileCardRole);
            _player.Camp.Stockpile.Add(stockpileCard);
            _player.OutstandingActions.Add(RoleType.Architect);

            BuildingSite site = new BuildingSite(stockpileCardMaterial);
            OrderCardModel building = new OrderCardModel("building card", "test", stockpileCardRole);
            site.BuildingFoundation.Add(building);
            _player.ConstructionZone.Add(site);

            var moveSpace = _action.Execute();

            var moves =
                moveSpace.Where(moveCollection => ((IMove<OrderCardModel>) moveCollection).Destination == site.Materials);

            var enumerator = moves.GetEnumerator();
            enumerator.MoveNext();
            // there should only be one move, to move the hand card to the concrete build site
            var moveCombo = enumerator.Current;
            Assert.IsInstanceOfType(moveCombo, typeof (IMove<OrderCardModel>));
            var feedAction = moveCombo as IMove<OrderCardModel>;

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(feedAction.Card, stockpileCard);
            Assert.AreEqual(feedAction.Source, _player.Camp.Stockpile);
            Assert.AreEqual(feedAction.Destination, site.Materials);

            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}