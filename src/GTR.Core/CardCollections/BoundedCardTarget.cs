#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class BoundedCardTarget<T> : CardLocation<T>, ICardTarget<T> where T : CardModelBase
    {
        private string _locationName;
        private int _maxCapacity;

        public BoundedCardTarget(string name = "")
        {
            _locationName = name;
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

        public override string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }
    }
}