#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public abstract class CardLocation<T> : ObservableCollection<T>, ICardLocation<T> where T : CardModelBase
    {
        private static int nextId;

        internal CardLocation()
        {
        }

        internal CardLocation(string id) : this()
        {
            Id = id;
        }

        internal CardLocation(IEnumerable<T> cards)
            : base(cards)
        {
        }

        public string Id { get; set; }
    }
}