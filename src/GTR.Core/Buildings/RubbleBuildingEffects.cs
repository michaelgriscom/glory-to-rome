#region

using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Buildings
{
    internal class BarEffect : BuildingEffectBase
    {
        private Deck<OrderCardModel> _deck;
        private Clientele _playerClientele;

        public override MaterialType Material
        {
            get { return MaterialType.Rubble; }
        }

        public override void ActivateBuilding(Player player, GameTable gameTable)
        {
            player.PlayerActions.Patron.PostActionMoves.Wrap(DeckPatron);
            _deck = gameTable.OrderDeck;
            _playerClientele = player.Camp.Clientele;
        }

        public override void DeactivateBuilding(Player player, GameTable gameTable)
        {
            player.PlayerActions.Patron.PostActionMoves.Unwrap(DeckPatron);
        }

        private IEnumerable<MoveSpace> DeckPatron(IAction action, IEnumerable<MoveSpace> arg)
        {
            if (_playerClientele.IsFull)
            {
                return arg;
            }
            MoveSpace moveSpace = new MoveSpace();

            IMove<OrderCardModel> deckHire = new Move<OrderCardModel>(_deck.Top, _deck, _playerClientele);
            moveSpace.Add(deckHire);

            IList<MoveSpace> newMoveSpace = new List<MoveSpace>(arg);
            newMoveSpace.Add(moveSpace);
            return newMoveSpace;
        }
    }

    internal class DockEffect : BuildingEffectBase
    {
        private Hand.OrderCardGroup _playerHand;
        private Stockpile _playerStockpile;

        public override MaterialType Material
        {
            get { return MaterialType.Wood; }
        }

        public override void ActivateBuilding(Player player, GameTable gameTable)
        {
            player.PlayerActions.Laborer.PostActionMoves.Wrap(HandLabor);
            _playerHand = player.Hand.OrderCards;
            _playerStockpile = player.Camp.Stockpile;
        }

        public override void DeactivateBuilding(Player player, GameTable gameTable)
        {
            player.PlayerActions.Laborer.PostActionMoves.Unwrap(HandLabor);
        }

        private IEnumerable<MoveSpace> HandLabor(IAction action, IEnumerable<MoveSpace> arg)
        {
            MoveSpace moveSpace = new MoveSpace();
            foreach (OrderCardModel handCard in _playerHand)
            {
                IMove<OrderCardModel> laborMove = new Move<OrderCardModel>(handCard, _playerHand, _playerStockpile);
                moveSpace.Add(laborMove);
            }

            IList<MoveSpace> newMoveSpace = new List<MoveSpace>(arg);
            newMoveSpace.Add(moveSpace);
            return newMoveSpace;
        }
    }

    internal class FountainEffect : BuildingEffectBase
    {
        private Deck<OrderCardModel> _deck;
        private Hand.OrderCardGroup _playerHand;

        public override MaterialType Material
        {
            get { return MaterialType.Marble; }
        }

        public override void ActivateBuilding(Player player, GameTable gameTable)
        {
            _deck = gameTable.OrderDeck;
            _playerHand = player.Hand.OrderCards;

            player.PlayerActions.Craftsman.PreActionMoves.Wrap(HandCard);
        }

        public override void DeactivateBuilding(Player player, GameTable gameTable)
        {
            player.PlayerActions.Craftsman.PreActionMoves.Unwrap(HandCard);
        }

        private IEnumerable<MoveSpace> HandCard(IEnumerable<MoveSpace> arg)
        {
            MoveSpace moveSpace = new MoveSpace();

            IMove<OrderCardModel> handMove = new Move<OrderCardModel>(_deck.Top, _deck, _playerHand);
            moveSpace.Add(handMove);

            IList<MoveSpace> newMoveSpace = new List<MoveSpace>(arg);
            newMoveSpace.Add(moveSpace);
            return newMoveSpace;
        }
    }

    internal class TempleEffect : BuildingEffectBase
    {
        public override MaterialType Material
        {
            get { return MaterialType.Marble; }
        }

        public override void ActivateBuilding(Player player, GameTable gameTable)
        {
            player.Hand.RefillCapacity += 4;
        }

        public override void DeactivateBuilding(Player player, GameTable gameTable)
        {
            player.Hand.RefillCapacity -= 4;
        }
    }
}