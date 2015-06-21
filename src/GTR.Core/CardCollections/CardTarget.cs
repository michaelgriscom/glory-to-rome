#region

using System.Collections.ObjectModel;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public abstract class CardTarget<T> : ObservableCollection<T>, ICardTarget<T> where T : CardModelBase
    {
        public CardTarget(string name = "")
        {
            LocationName = name;
        }

        public virtual bool CanAdd(T card)
        {
            return true;
        }

        public string LocationName { get; set; }
    }
}