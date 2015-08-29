#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public interface ICardTarget<T> : ICardLocation<T> where T : CardModelBase
    {
        void Add(T card);
        bool CanAdd(T card);
    }
}