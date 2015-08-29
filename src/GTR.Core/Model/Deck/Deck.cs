#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GTR.Core.CardCollections;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model
{
    public class Deck<T> : ObservableModel, ICardSource<T> where T : CardModelBase
    {
        private ObservableCollection<T> _cards;

        internal Deck(IEnumerable<T> cards)
        {
            Cards = new ObservableCollection<T>(cards);
        }

        public Deck()
            : this(new ObservableCollection<T>())
        {
            // empty deck
        }

        public ObservableCollection<T> Cards
        {
            get { return _cards; }
            private set
            {
                _cards = value;
                RaisePropertyChanged();
            }
        }

        internal int Count
        {
            get { return _cards.Count; }
        }

        internal T Top
        {
            get { return ElementAt(0); }
        }

        int ICardSource<T>.Count
        {
            get { return _cards.Count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        public T ElementAt(int index)
        {
            return _cards[0];
        }

        public void RemoveAt(int index)
        {
            _cards.RemoveAt(0);
        }

        public string Id
        {
            get { return "Deck"; }
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

        internal void AddToTop(T card)
        {
            _cards.Insert(0, card);
        }

        internal T Draw()
        {
            T topCard = Top;
            _cards.RemoveAt(0);
            return topCard;
        }

        /// <summary>
        ///     Code obtained from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// </summary>
        internal void Shuffle()
        {
            Random randGen = new Random();
            List<T> cardList = new List<T>(_cards);
            int numRemaining = _cards.Count;
            while (numRemaining > 1)
            {
                numRemaining--;
                int randIndex = randGen.Next(numRemaining + 1);
                T value = cardList[randIndex];
                cardList[randIndex] = cardList[numRemaining];
                cardList[numRemaining] = value;
            }
            _cards.Clear();
            foreach (T card in cardList)
            {
                _cards.Add(card);
            }
        }
    }
}