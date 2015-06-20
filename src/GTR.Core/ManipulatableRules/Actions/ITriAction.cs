﻿#region

using System.Collections.Generic;
using GTR.Core.Buildings;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    public interface ITriAction
    {
        WrappedFunc<MoveSpace> ActionMoves { get; }
        WrappedFunc<IMove<CardModelBase>, IEnumerable<MoveSpace>> PostActionMoves { get; }
        WrappedFunc<IEnumerable<MoveSpace>> PreActionMoves { get; }
        event PlayerActionHandler OnAction;
        event PlayerActionHandler OnPostAction;
        event PlayerActionHandler OnPreAction;
    }
}