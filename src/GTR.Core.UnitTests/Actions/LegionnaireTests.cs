#region

using System.Collections.Generic;
using System.Linq;
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
    public class LegionnaireTests
    {
        private LegionnaireAction _action;
        private GameTable _gameTable;
        private Player _player;

        [TestInitialize]
        public void Initialization()
        {
            OrderDeck orderDeck = new OrderDeck();
            JackDeck jackDeck = new JackDeck();

            _gameTable = new GameTable(orderDeck, jackDeck);

            PlayerInputForTest input = new PlayerInputForTest();
            _player = new Player("test-player");

            _action = new LegionnaireAction(_player, _gameTable);
        }

        [TestMethod]
        public void LegionnaireDemandSpace()
        {
            var players = new List<Player> {_player};
            _gameTable.AddPlayers(players);

            const RoleType handCardRole = RoleType.Craftsman;

            OrderCardModel demandCard = new OrderCardModel("hand card", "test", handCardRole);
            _player.Hand.OrderCards.Add(demandCard);
            _player.OutstandingActions.Add(RoleType.Legionnaire);
            var moveSpace = _action.Execute();

            var enumerator = moveSpace.GetEnumerator();
            enumerator.MoveNext();

            // there should only be one move, to move the hand card to the demand area
            var moveCombo = enumerator.Current;
            Assert.IsInstanceOfType(moveCombo, typeof (IMove<OrderCardModel>));
            var move = moveCombo as IMove<OrderCardModel>;
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(move.Card, demandCard);
            Assert.AreEqual(move.Source, _player.Hand.OrderCards);
            Assert.AreEqual(move.Destination, _player.DemandArea);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void LegionnaireSingleTargetWithCard()
        {
            PlayerInputForTest enemyPlayerInput = new PlayerInputForTest();
            var enemyPlayer = new Player("enemy-player");

            List<Player> players = new List<Player> {enemyPlayer, _player};
            _gameTable.AddPlayers(players);

            RoleType handCardRole = RoleType.Craftsman;

            OrderCardModel demandCard = new OrderCardModel("hand card", "test", handCardRole);
            _player.Hand.OrderCards.Add(demandCard);

            OrderCardModel demandedCard = new OrderCardModel("hand card", "test", handCardRole);
            enemyPlayer.Hand.OrderCards.Add(demandedCard);

            IMove<OrderCardModel> demandMove = new Move<OrderCardModel>(demandCard, _player.Hand.OrderCards,
                _player.DemandArea);
            demandMove.Perform();

            enemyPlayerInput.OnMoveEventHandler +=
                (sender, args) =>
                {
                    Assert.IsTrue(args.MoveSpace.IsRequired);

                    Assert.IsInstanceOfType(args.MoveSpace.ElementAt(0), typeof (IMove<OrderCardModel>));
                    var demandedMove = args.MoveSpace.ElementAt(0) as IMove<OrderCardModel>;

                    // ReSharper disable once PossibleNullReferenceException
                    Assert.AreEqual(demandedMove.Card, demandedCard);
                    Assert.AreEqual(demandedMove.Source, enemyPlayer.Hand.OrderCards);
                    Assert.AreEqual(demandedMove.Destination, _player.Camp.Stockpile);
                };
        }

        [TestMethod]
        public void LegionnaireSingleTargetWithCards()
        {
            PlayerInputForTest enemyPlayerInput = new PlayerInputForTest();
            var enemyPlayer = new Player("enemy-player");

            List<Player> players = new List<Player> {enemyPlayer, _player};
            _gameTable.AddPlayers(players);

            RoleType handCardRole = RoleType.Craftsman;

            OrderCardModel demandCard = new OrderCardModel("hand card", "test", handCardRole);
            _player.Hand.OrderCards.Add(demandCard);

            OrderCardModel demandedCard1 = new OrderCardModel("hand card", "test", handCardRole);
            enemyPlayer.Hand.OrderCards.Add(demandedCard1);
            OrderCardModel demandedCard2 = new OrderCardModel("hand card", "test", handCardRole);
            enemyPlayer.Hand.OrderCards.Add(demandedCard2);

            IMove<OrderCardModel> demandMove = new Move<OrderCardModel>(demandCard, _player.Hand.OrderCards,
                _player.DemandArea);
            demandMove.Perform();

            enemyPlayerInput.OnMoveEventHandler +=
                (sender, args) =>
                {
                    Assert.IsTrue(args.MoveSpace.IsRequired);
                    int possibleMoves = 0;
                    foreach (var moveCombo in args.MoveSpace)
                    {
                        possibleMoves++;

                        Assert.IsInstanceOfType(moveCombo, typeof (IMove<OrderCardModel>));
                        var demandedMove = moveCombo as IMove<OrderCardModel>;

                        // ReSharper disable once PossibleNullReferenceException
                        bool isDemandable = demandedMove.Card == demandedCard1 || demandedMove.Card == demandedCard2;
                        Assert.IsTrue(isDemandable);
                        Assert.AreEqual(demandedMove.Source, enemyPlayer.Hand.OrderCards);
                        Assert.AreEqual(demandedMove.Destination, _player.Camp.Stockpile);
                    }
                    Assert.AreEqual(2, possibleMoves);
                };
        }

        [TestMethod]
        public void LegionnaireSingleTargetWithoutCard()
        {
            PlayerInputForTest enemyPlayerInput = new PlayerInputForTest();
            var enemyPlayer = new Player("enemy-player");

            List<Player> players = new List<Player> {enemyPlayer, _player};
            _gameTable.AddPlayers(players);

            RoleType demandRoleType = RoleType.Craftsman;

            var enemyCardTypes = new[]
            {
                RoleType.Architect,
                RoleType.Laborer,
                RoleType.Legionnaire,
                RoleType.Merchant,
                RoleType.Patron
            };

            OrderCardModel demandCard = new OrderCardModel("hand card", "test", demandRoleType);
            _player.Hand.OrderCards.Add(demandCard);
            foreach (var role in enemyCardTypes)
            {
                OrderCardModel demandedCard = new OrderCardModel("hand card", "test", role);
                enemyPlayer.Hand.OrderCards.Add(demandedCard);
            }

            IMove<OrderCardModel> demandMove = new Move<OrderCardModel>(demandCard, _player.Hand.OrderCards,
                _player.DemandArea);
            demandMove.Perform();

            enemyPlayerInput.OnMoveEventHandler +=
                (sender, args) => { Assert.AreEqual(0, args.MoveSpace.Count()); };
        }
    }
}