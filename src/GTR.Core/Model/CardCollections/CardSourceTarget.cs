#region

using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class CardSourceTarget<T> : CardLocation<T>, ICardTarget<T>, ICardSource<T> where T : CardModelBase
    {
        private string _locationName;

        public CardSourceTarget(string name = "")
        {
            _locationName = name;
        }

        public CardSourceTarget(IEnumerable<T> cards) : base(cards)
        {
        }

        public T ElementAt(int index)
        {
            return this[index];
        }

        public virtual bool CanAdd(T card)
        {
            return true;
        }

        public override string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }
    }
}