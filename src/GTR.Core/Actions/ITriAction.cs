#region

using System.Collections.Generic;
using GTR.Core.Buildings;
using GTR.Core.Moves;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Actions
{
    public interface ITriAction
    {
        WrappedFunc<MoveSpace> ActionMoves { get; }
        WrappedFunc<IAction, IEnumerable<MoveSpace>> PostActionMoves { get; }
        WrappedFunc<IEnumerable<MoveSpace>> PreActionMoves { get; }
        event PlayerActionHandler OnAction;
        event PlayerActionHandler OnPostAction;
        event PlayerActionHandler OnPreAction;
    }
}