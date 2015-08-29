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
            Id = name;
        }

        public virtual bool CanAdd(T card)
        {
            return true;
        }

        public string Id { get; set; }
    }
}