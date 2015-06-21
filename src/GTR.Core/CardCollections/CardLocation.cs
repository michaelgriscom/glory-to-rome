#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public abstract class CardLocation<T> : ObservableCollection<T>, ICardLocation<T> where T : CardModelBase
    {
        private string _locationName;

        internal CardLocation()
        {
        }

        internal CardLocation(string name)
        {
            _locationName = name;
        }

        internal CardLocation(IEnumerable<T> cards)
            : base(cards)
        {
        }

        public virtual string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }
    }
}