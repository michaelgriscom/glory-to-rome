#region

using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Actions;
using GTR.Core.CardCollections;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.UnitTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests.Actions
{
    [TestClass]
    public class LaborerTests
    {
        private LaborerAction _action;
        private GameTable _gameTable;
        private Player _player;

        [TestInitialize]
        public void Initialization()
        {
            OrderDeck orderDeck = new OrderDeck();
            JackDeck jackDeck = new JackDeck();

            _gameTable = new GameTable(orderDeck, jackDeck);
            PlayerInputForTest input = new PlayerInputForTest();

            _player = new Player("test-player", input);
            _gameTable.AddPlayers(new List<Player> {_player});

            _action = new LaborerAction(_player, _gameTable);
        }

        [TestMethod]
        public void VanillaLaborer()
        {
            OrderCardModel poolCard = new OrderCardModel("pool card", "test", RoleType.Architect);
            _gameTable.Pool.Add(poolCard);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            enumerator.MoveNext();

            // there should only be one move, to move the pool card to the stockpile
            var moveCombo = enumerator.Current;
            Assert.IsInstanceOfType(moveCombo, typeof (IMove<OrderCardModel>));

            var move = moveCombo as IMove<OrderCardModel>;
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(move.Card, poolCard);
            Assert.AreEqual(move.Source, _gameTable.Pool);
            Assert.AreEqual(move.Destination, _player.Camp.Stockpile);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void MultipleOptionsLaborer()
        {
            const int poolCount = 5;
            HashSet<OrderCardModel> poolCards = new HashSet<OrderCardModel>();
            for (int i = 0; i < poolCount; i++)
            {
                OrderCardModel poolCard = new OrderCardModel("pool card", "test", RoleType.Patron);
                _gameTable.Pool.Add(poolCard);
                poolCards.Add(poolCard);
            }

            var moveSpace = _action.Execute();

            int moveCount = 0;
            foreach (var moveCollection in moveSpace)
            {
                moveCount++;

                Assert.IsInstanceOfType(moveCollection, typeof (IMove<OrderCardModel>));

                var move = moveCollection as IMove<OrderCardModel>;

                // ReSharper disable once PossibleNullReferenceException
                Assert.IsTrue(poolCards.Contains(move.Card));
                Assert.AreEqual(move.Source, _gameTable.Pool);
                Assert.AreEqual(move.Destination, _player.Camp.Stockpile);
            }

            Assert.AreEqual(poolCount, moveCount);
        }

        [TestMethod]
        public void EmptyPoolLaborer()
        {
            var moveSpace = _action.Execute();
            var enumerator = moveSpace.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}