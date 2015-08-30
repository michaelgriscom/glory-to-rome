#region

using System.Collections.Generic;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public interface ICardCollection<T> : IModel, ICollection<T>where T : CardModelBase
    {
        T ElementAt(int index);
        void RemoveAt(int index);
        string Id { get; set; }
    }
}