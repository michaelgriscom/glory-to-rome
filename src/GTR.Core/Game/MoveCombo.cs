#region

using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Game
{
    public class MoveCombo : List<IMove<CardModelBase>>
    {
        public MoveCombo(IMove<CardModelBase> move)
        {
            Add(move);
        }

        public MoveCombo()
        {
        }
    }
}