#region

using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Buildings;
using GTR.Core.Game;
using GTR.Core.Util;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    public abstract class OrderActionBase : ITriAction
    {
        protected readonly WrappedFunc<MoveSpace> actionMoves;
        protected readonly WrappedFunc<IAction, IEnumerable<MoveSpace>> postActionMoves;
        protected readonly WrappedFunc<IEnumerable<MoveSpace>> preActionMoves;
        protected GameTable GameTable;
        protected Player Player;

        internal OrderActionBase(Player player, GameTable gameTable)
        {
            Player = player;
            GameTable = gameTable;
            preActionMoves = new WrappedFunc<IEnumerable<MoveSpace>>(GetPremoveSpaces);
            actionMoves = new WrappedFunc<MoveSpace>(GetMoveSpace);
            postActionMoves = new WrappedFunc<IAction, IEnumerable<MoveSpace>>(GetPostmoveSpaces);
        }

        public event PlayerActionHandler OnAction;
        public event PlayerActionHandler OnPostAction;
        public event PlayerActionHandler OnPreAction;

        public WrappedFunc<MoveSpace> ActionMoves
        {
            get { return actionMoves; }
        }

        public WrappedFunc<IAction, IEnumerable<MoveSpace>> PostActionMoves
        {
            get { return postActionMoves; }
        }

        public WrappedFunc<IEnumerable<MoveSpace>> PreActionMoves
        {
            get { return preActionMoves; }
        }

        internal IEnumerable<MoveSpace> Begin()
        {
            if (OnPreAction != null)
            {
                var args = GetActionEventArgs();
                OnPreAction(this, args);
            }
            var preMovesResult = preActionMoves.Execute();
            return preMovesResult;
        }

        internal MoveSpace Execute()
        {
            if (OnAction != null)
            {
                var args = GetActionEventArgs();
                OnAction(this, args);
            }
            var actionMoveResult = actionMoves.Execute();
            return actionMoveResult;
        }

        internal IEnumerable<MoveSpace> Complete(IAction action)
        {
            if (OnPostAction != null)
            {
                var args = GetActionEventArgs();
                OnPostAction(this, args);
            }

            var postMoveResult = postActionMoves.Execute(action);
            return postMoveResult;
        }

        private PlayerActionEventArgs GetActionEventArgs()
        {
            PlayerActionEventArgs args = new PlayerActionEventArgs
            {
                GameTable = GameTable,
                Player = Player
            };
            return args;
        }

        private void AddMoves(IEnumerable<MoveSpace> moveSpaces, Player player)
        {
            foreach (var moveSpace in moveSpaces)
            {
                player.AddMoveSpace(moveSpace);
            }
        }

        protected abstract MoveSpace GetMoveSpace();

        protected virtual IEnumerable<MoveSpace> GetPostmoveSpaces(IAction move)
        {
            return new List<MoveSpace>();
        }

        protected virtual IEnumerable<MoveSpace> GetPremoveSpaces()
        {
            return new List<MoveSpace>();
        }
    }
}