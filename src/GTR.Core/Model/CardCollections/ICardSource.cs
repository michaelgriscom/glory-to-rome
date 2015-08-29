#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public interface ICardSource<out T> : ICardLocation<T> where T : CardModelBase
    {
        int Count { get; }
        T ElementAt(int index);
        void RemoveAt(int index);
    }
}