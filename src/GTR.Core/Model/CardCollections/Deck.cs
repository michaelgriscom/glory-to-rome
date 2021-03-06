﻿#region

using System;
using System.Collections.Generic;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public class Deck<T> : ObservableCardCollection<T> where T : CardModelBase
    {
        public Deck()
        {
        }

        internal Deck(ICardCollection<T> cl) : base(cl)
        {
        }

        public T Top
        {
            get { return ElementAt(0); }
        }

        internal T Draw()
        {
            T topCard = Top;
            RemoveAt(0);
            return topCard;
        }

        /// <summary>
        ///     Code obtained from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// </summary>
        internal void Shuffle()
        {
            Random randGen = new Random();
            List<T> cardList = new List<T>(this);
            int numRemaining = Count;
            while (numRemaining > 1)
            {
                numRemaining--;
                int randIndex = randGen.Next(numRemaining + 1);
                T value = cardList[randIndex];
                cardList[randIndex] = cardList[numRemaining];
                cardList[numRemaining] = value;
            }
            Clear();
            foreach (T card in cardList)
            {
                Add(card);
            }
        }
    }
}