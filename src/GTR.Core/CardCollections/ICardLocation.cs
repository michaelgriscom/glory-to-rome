#region

using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public interface ICardLocation<out T> : IEnumerable<T> where T : CardModelBase
    {
        string LocationName { get; }
    }
}