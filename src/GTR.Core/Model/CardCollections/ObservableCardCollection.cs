#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public class ObservableCardCollection<T> : ObservableCollection<T>, ICardCollection<T>, INotifyPropertyChanged
        where T : CardModelBase
    {
        public ObservableCardCollection()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ObservableCardCollection(IEnumerable<T> cards) : base(cards)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ObservableCardCollection(ICardCollection<T> cl) : base(cl)
        {
            this.Id = cl.Id;
        }

        public T ElementAt(int index)
        {
            return this[index];
        }

        public string Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}