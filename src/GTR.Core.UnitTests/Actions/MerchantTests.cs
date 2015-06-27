﻿#region

using System.Collections.Generic;
using GTR.Core.Action;
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
    public class MerchantTests
    {
        private MerchantAction _action;
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
            _gameTable.AddPlayers(new List<Player> {_player});

            _action = new MerchantAction(_player, _gameTable);
        }

        [TestMethod]
        public void VanillaMerchant()
        {
            _player.Board.Camp.Vault.MaxCapacity = 1;
            var action = new MerchantAction(_player, _gameTable);

            OrderCardModel stockpileCard = new OrderCardModel("pool card", "test", RoleType.Architect);
            _player.Board.Camp.Stockpile.Add(stockpileCard);
            var moveSpace = action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            enumerator.MoveNext();

            // there should only be one move, to move the pool card to the stockpile
            var moveCombo = enumerator.Current;
            Assert.IsInstanceOfType(moveCombo, typeof (IMove<OrderCardModel>));
            var move = moveCombo as IMove<OrderCardModel>;

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(move.Card, stockpileCard);
            Assert.AreEqual(move.Source, _player.Board.Camp.Stockpile);
            Assert.AreEqual(move.Destination, _player.Board.Camp.Vault);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void FullVaultMerchant()
        {
            _player.Board.Camp.Vault.MaxCapacity = 0;

            OrderCardModel stockpileCard = new OrderCardModel("pool card", "test", RoleType.Architect);
            _player.Board.Camp.Stockpile.Add(stockpileCard);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void EmptyStockpileMerchant()
        {
            _player.Board.Camp.Vault.MaxCapacity = 1;
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void MultipleOptionsMerchant()
        {
            _player.Board.Camp.Vault.MaxCapacity = 1;

            const int stockpileCount = 5;
            HashSet<OrderCardModel> stockpileCards = new HashSet<OrderCardModel>();
            for (int i = 0; i < stockpileCount; i++)
            {
                OrderCardModel stockpileCard = new OrderCardModel("pool card", "test", RoleType.Craftsman);
                _player.Board.Camp.Stockpile.Add(stockpileCard);
                stockpileCards.Add(stockpileCard);
            }

            var moveSpace = _action.Execute();

            int moveCount = 0;
            foreach (var moveCollection in moveSpace)
            {
                moveCount++;

                Assert.IsInstanceOfType(moveCollection, typeof (IMove<OrderCardModel>));
                var move = moveCollection as IMove<OrderCardModel>;

                // ReSharper disable once PossibleNullReferenceException
                Assert.IsTrue(stockpileCards.Contains(move.Card));
                Assert.AreEqual(move.Source, _player.Board.Camp.Stockpile);
                Assert.AreEqual(move.Destination, _player.Board.Camp.Vault);
            }

            Assert.AreEqual(stockpileCount, moveCount);
        }
    }
}