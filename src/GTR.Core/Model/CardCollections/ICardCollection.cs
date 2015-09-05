#region

using System.Collections.Generic;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public interface ICardCollection<T> : IModel, ICollection<T> where T : CardModelBase
    {
        string Id { get; set; }
        T ElementAt(int index);
        void RemoveAt(int index);
    }
}