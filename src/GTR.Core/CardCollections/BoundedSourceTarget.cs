#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class BoundedSourceTarget<T> : BoundedCardTarget<T>, ICardSource<T> where T : CardModelBase
    {
        public BoundedSourceTarget(int capacity) : base(capacity)
        {
        }

        public BoundedSourceTarget()
        {
        }

        public T ElementAt(int index)
        {
            return this[index];
        }
    }
}