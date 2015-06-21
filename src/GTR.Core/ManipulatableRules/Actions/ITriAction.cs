#region

using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Buildings;
using GTR.Core.Util;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
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