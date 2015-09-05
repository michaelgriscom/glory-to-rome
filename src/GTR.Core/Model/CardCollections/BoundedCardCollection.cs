﻿#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public class BoundedCardCollection<T> : ObservableObject, ICardCollection<T> where T : CardModelBase
    {
        private readonly ICardCollection<T> collection;
        private int _capacity;

        public BoundedCardCollection(ICardCollection<T> collection)
        {
            this.collection = collection;
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            collection.Add(item);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return collection.Remove(item);
        }

        public int Count => collection.Count;
        public bool IsReadOnly => collection.IsReadOnly;

        public T ElementAt(int index)
        {
            return collection.ElementAt(index);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
        }

        public int Id
        {
            get { return collection.Id; }
            set
            {
                collection.Id = value;
                RaisePropertyChanged();
            }
        }
    }
}