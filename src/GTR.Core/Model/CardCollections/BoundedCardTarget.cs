#region

using System;
using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public interface IBoundable
    {
        int Capacity { get; set; }
    }

    public interface IConditionalAddable<T> where T : CardModelBase
    {
        bool CanAdd(T card);
    }


    public class BoundedCardCollection<T> : ObservableCardCollection<T>, IBoundable where T : CardModelBase
    {
        private int _capacity;
        private ICardCollection<T> collection;

        public BoundedCardCollection(ICardCollection<T> collection)
        {
            this.collection = collection;
        }

        public BoundedCardCollection()
        {
            this.collection = new ObservableCardCollection<T>();
        } 

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public void Add(T card)
        {
            if (!CanAdd(card))
            {
                throw new ArgumentException("Collection is full");
            }
            collection.Add(card);
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

        public virtual bool CanAdd(T card)
        {
            return Count < Capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public string Id
        {
            get { return collection.Id; }
            set { collection.Id = value; }
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool IsReadOnly
        {
            get
            {
                return collection.IsReadOnly;
            }
        }

        public T ElementAt(int index)
        {
            return collection.ElementAt(index);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
        }
    }
}