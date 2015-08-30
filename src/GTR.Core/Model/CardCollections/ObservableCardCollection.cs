#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class ObservableCardCollection<T> : ObservableCollection<T>, ICardCollection<T>, INotifyPropertyChanged where T : CardModelBase
    {
        // Check the attribute in the following line :
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCardCollection() : base()
        {
        }

        public ObservableCardCollection(IEnumerable<T> cards) : base(cards)
        {
        }

        public T ElementAt(int index)
        {
            return this[index];
        }

        //public virtual bool CanAdd(T card)
        //{
        //    return true;
        //}

        public string Id { get; set; }
    }

}