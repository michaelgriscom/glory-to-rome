#region

using System.Collections.Generic;
using System.Linq;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.ManipulatableRules.Actions;
using GTR.Core.Model;
using GTR.Core.UnitTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests.Actions
{
    [TestClass]
    public class CraftsmanTests
    {
        private CraftsmanAction _action;
        private GameTable _gameTable;
        private Player _player;

        [TestInitialize]
        public void Initialization()
        {
            Deck<OrderCardModel> orderDeck = new Deck<OrderCardModel>();
            CardSourceTarget<JackCardModel> jackDeck = new CardSourceTarget<JackCardModel>();

            _gameTable = new GameTable(orderDeck, jackDeck);


            PlayerInputForTest input = new PlayerInputForTest();

            _player = new Player("test-player", input);
            List<Player> players = new List<Player> {_player}
                ;
            _gameTable.AddPlayers(players);
            _action = new CraftsmanAction(_player, _gameTable);
        }

        [TestMethod]
        public void BuildCraftsman()
        {
            RoleType handCardRole = RoleType.Architect;
            MaterialType handCardMaterial = handCardRole.ToMaterial();

            OrderCardModel handCard = new OrderCardModel("hand card", "test", handCardRole);
            _player.Hand.OrderCards.Add(handCard);
            _player.OutstandingActions.Add(RoleType.Craftsman);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            enumerator.MoveNext();

            // there should be two moves, to move the hand card to the build site and to move the build site to the construction zone
            var moveCombo = enumerator.Current;
            Assert.AreEqual(2, moveCombo.Count);

            var buildAction = moveCombo.ElementAt(0);
            Assert.AreEqual(buildAction.Card, handCard);
            Assert.AreEqual(buildAction.Source, _player.Hand.OrderCards);
            Assert.AreEqual(buildAction.Destination, _gameTable.GetSiteDeck(handCardMaterial).Top.BuildingFoundation);

            var siteMove = moveCombo.ElementAt(1);
            Assert.AreEqual(siteMove.Card, _gameTable.GetSiteDeck(handCardMaterial).Top);
            Assert.AreEqual(siteMove.Source, _gameTable.GetSiteDeck(handCardMaterial));
            Assert.AreEqual(siteMove.Destination, _player.ConstructionZone);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void FeedCraftsman()
        {
            RoleType handCardRole = RoleType.Legionnaire;
            MaterialType handCardMaterial = handCardRole.ToMaterial();

            OrderCardModel handCard = new OrderCardModel("hand card", "test", handCardRole);
            _player.Hand.OrderCards.Add(handCard);
            _player.OutstandingActions.Add(RoleType.Craftsman);

            BuildingSite site = new BuildingSite(handCardMaterial);
            OrderCardModel building = new OrderCardModel("building card", "test", handCardRole);
            site.BuildingFoundation.Add(building);
            _player.ConstructionZone.Add(site);

            var moveSpace = _action.Execute();

            var moves = moveSpace.Where(moveCollection => moveCollection.ElementAt(0).Destination == site.Materials);

            var enumerator = moves.GetEnumerator();
            enumerator.MoveNext();
            // there should only be one move, to move the hand card to the concrete build site
            var moveCombo = enumerator.Current;
            Assert.AreEqual(1, moveCombo.Count);

            var feedAction = moveCombo.ElementAt(0);
            Assert.AreEqual(feedAction.Card, handCard);
            Assert.AreEqual(feedAction.Source, _player.Hand.OrderCards);
            Assert.AreEqual(feedAction.Destination, site.Materials);

            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}