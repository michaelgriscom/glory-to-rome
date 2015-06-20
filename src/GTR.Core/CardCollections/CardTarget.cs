#region

using System.Collections.ObjectModel;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class CardTarget<T> : ObservableCollection<T>, ICardTarget<T> where T : CardModelBase
    {
        public virtual bool CanAdd(T card)
        {
            return true;
        }
    }
}