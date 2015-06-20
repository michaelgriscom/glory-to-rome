#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class CardLocation<T> : ObservableCollection<T>, ICardLocation<T> where T : CardModelBase
    {
        internal CardLocation()
        {
        }

        internal CardLocation(IEnumerable<T> cards)
            : base(cards)
        {
        }
    }
}