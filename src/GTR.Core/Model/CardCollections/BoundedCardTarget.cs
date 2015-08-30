#region

using System;
using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Util;

#endregion

namespace GTR.Core.CardCollections
{
    public class BoundedCardCollection<T> : ObservableObject, ICardCollection<T> where T : CardModelBase
    {
        private int _capacity;
        private ICardCollection<T> collection;

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; RaisePropertyChanged();}
        }

        public BoundedCardCollection(ICardCollection<T> collection)
        {
            this.collection = collection;
        } 

        public IEnumerator<T> GetEnumerator()
        {
           return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            this.collection.Add(item);
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

        public string Id
        {
            get { return collection.Id; }
            set { collection.Id = value;  RaisePropertyChanged();}
        }
    }
}