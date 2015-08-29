#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public abstract class CardLocation<T> : ObservableCollection<T>, ICardLocation<T> where T : CardModelBase
    {
        private string _locationName;

        public int Id { get; private set; }

        static int nextId;

        internal CardLocation()
        {
            Id = Interlocked.Increment(ref nextId);
        }

        internal CardLocation(string name) : this()
        {
            _locationName = name;
        }

        internal CardLocation(IEnumerable<T> cards)
            : base(cards)
        {
            Id = Interlocked.Increment(ref nextId);
        }

        public virtual string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }
    }
}