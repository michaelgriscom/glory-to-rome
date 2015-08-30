#region

using System.Collections.Generic;
using GTR.Core.Model;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.CardCollections
{

    public interface ICardCollection<T> : ICollection<T>, ICardLocation<T> where T : CardModelBase
    {

    }

    public interface ICardLocation<out T> : IModel, IEnumerable<T> where T : CardModelBase
    {
        T ElementAt(int index);
        void RemoveAt(int index);
        string Id { get; set; }
    }
}