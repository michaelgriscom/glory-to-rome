#region

using System.Collections.Generic;
using GTR.Core.Model;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.CardCollections
{
    public interface ICardLocation<out T> : IModel, IEnumerable<T> where T : CardModelBase
    {
        string Id { get; }
    }
}