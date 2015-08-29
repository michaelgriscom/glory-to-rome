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
        private string _id;

        static int nextId;

        internal CardLocation()
        {
        }

        internal CardLocation(string id) : this()
        {
            _id = id;
        }

        internal CardLocation(IEnumerable<T> cards)
            : base(cards)
        {
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}