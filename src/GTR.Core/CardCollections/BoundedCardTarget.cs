#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class BoundedCardTarget<T> : CardLocation<T>, ICardTarget<T> where T : CardModelBase
    {
        private int _maxCapacity;

        public BoundedCardTarget()
        {
        }

        public BoundedCardTarget(int maxCapacity)
        {
            _maxCapacity = maxCapacity;
        }

        public virtual int MaxCapacity
        {
            get { return _maxCapacity; }
            set { _maxCapacity = value; }
        }

        public virtual bool CanAdd(T card)
        {
            return Count < MaxCapacity;
        }
    }
}