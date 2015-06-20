﻿#region

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
    public class PatronTests
    {
        private PatronAction _action;
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
            _gameTable.AddPlayers(new List<Player>() { _player });
            _action = new PatronAction(_player, _gameTable);
        }

        [TestMethod]
        public void VanillaPatron()
        {
            _player.Camp.Clientele.MaxCapacity = 1;

            OrderCardModel poolCard = new OrderCardModel("pool card", "test", RoleType.Architect);
            _gameTable.Pool.Add(poolCard);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            enumerator.MoveNext();

            // there should only be one move, to move the pool card to the clientele
            var moveCombo = enumerator.Current;
            Assert.AreEqual(1, moveCombo.Count);

            var move = moveCombo.ElementAt(0);
            Assert.AreEqual(move.Card, poolCard);
            Assert.AreEqual(move.Source, _gameTable.Pool);
            Assert.AreEqual(move.Destination, _player.Camp.Clientele);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void FullClientelePatron()
        {
            _player.Camp.Clientele.MaxCapacity = 0;

            OrderCardModel poolCard = new OrderCardModel("pool card", "test", RoleType.Architect);
            _gameTable.Pool.Add(poolCard);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void EmptyPoolPatron()
        {
            _player.Camp.Clientele.MaxCapacity = 1;

            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void MultipleOptionsPatron()
        {
            _player.Camp.Clientele.MaxCapacity = 1;

            const int poolCount = 5;
            HashSet<OrderCardModel> poolCards = new HashSet<OrderCardModel>();
            for (int i = 0; i < poolCount; i++)
            {
                OrderCardModel poolCard = new OrderCardModel("pool card", "test", RoleType.Laborer);
                _gameTable.Pool.Add(poolCard);
                poolCards.Add(poolCard);
            }

            var moveSpace = _action.Execute();

            int moveCount = 0;
            foreach (var moveCollection in moveSpace)
            {
                moveCount++;

                Assert.AreEqual(1, moveCollection.Count);

                var move = moveCollection.ElementAt(0);
                Assert.IsTrue(poolCards.Contains(move.Card));
                Assert.AreEqual(move.Source, _gameTable.Pool);
                Assert.AreEqual(move.Destination, _player.Camp.Clientele);
            }

            Assert.AreEqual(poolCount, moveCount);
        }
    }
}