#region

using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Action
{
    public interface IAction : IEnumerable<IMove<CardModelBase>>
    {
        bool Perform();
    }
}