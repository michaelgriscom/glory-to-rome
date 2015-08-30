#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GTR.Core.CardCollections;
using GTR.Core.Serialization;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class Deck<T> : ObservableCardCollection<CardModelBase> where T : CardModelBase
    {
      private ObservableCardCollection<T> collection;

        internal Deck(IEnumerable<T> cards)
        {
            collection = new ObservableCardCollection<T>(cards);
        }

        public Deck()
            : this(new ObservableCardCollection<T>())
        {
            // empty deck
        }

        public Deck(ObservableCardCollection<T> collection)
        {
            this.collection = collection;
        } 

        public ObservableCardCollection<T> Cards
        {
            get { return collection; }
            private set
            {
                collection = value;
                RaisePropertyChanged();
            }
        }

        internal int Count
        {
            get { return collection.Count; }
        }

        internal T Top
        {
            get { return collection.ElementAt(0); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public T ElementAt(int index)
        {
            return collection.ElementAt(index);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(0);
        }

        public string Id
        {
            get { return collection.Id; }
            set { collection.Id = value; }
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

        public bool Remove(T card)
        {
            // only allow removal from top
            bool cardIsOnTop = Top.Equals(card);
            if (cardIsOnTop)
            {
                Draw();
            }
            return cardIsOnTop;
        }

        internal T Draw()
        {
            T topCard = Top;
            collection.RemoveAt(0);
            return topCard;
        }

        /// <summary>
        ///     Code obtained from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// </summary>
        internal void Shuffle()
        {
            Random randGen = new Random();
            List<T> cardList = new List<T>(collection);
            int numRemaining = collection.Count;
            while (numRemaining > 1)
            {
                numRemaining--;
                int randIndex = randGen.Next(numRemaining + 1);
                T value = cardList[randIndex];
                cardList[randIndex] = cardList[numRemaining];
                cardList[numRemaining] = value;
            }
            collection.Clear();
            foreach (T card in cardList)
            {
                collection.Add(card);
            }
        }
    }
}