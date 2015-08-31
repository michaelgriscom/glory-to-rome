﻿#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.CardCollections
{
    public class ObservableCardCollection<T> : ObservableCollection<T>, ICardCollection<T>, INotifyPropertyChanged
        where T : CardModelBase
    {
        public ObservableCardCollection()
        {
        }

        public ObservableCardCollection(IEnumerable<T> cards) : base(cards)
        {
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