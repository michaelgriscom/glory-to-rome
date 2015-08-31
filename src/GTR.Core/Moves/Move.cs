#region

using System;
using System.Collections.Generic;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Moves
{
    public class Move<T> : IMove<T>, IModel where T : CardModelBase
    {
        private readonly Func<ICardCollection<T>> _destinationFunction;
        // this member should be treated as readonly after its initial assignment,
        // however we want to allow it to be evaluated lazily through a Func, so
        // we have to rely on this class not to mutate it after evaluation. Otherwise,
        // the hashcode could change and we could lose the object in a collection.
        private ICardCollection<T> _destination;

        public Move(T card, ICardCollection<T> source, ICardCollection<T> destination)
        {
            Card = card;
            Source = source;
            _destination = destination;
        }

        // consider: the constructor is pickier than it should be.
        // As long as source<U> destination<U> and T card have the relationship T:U
        // then it's a valid move. The problem is that C# doesn't support constraints in a constructor,
        // so we'd need a messy Initialize method with a signature like below. The problem though is that we'd have to expose U and V
        // through the getters, so this type will become really ugly. Instead, the ugliness is currently isolated to two main places,
        // the player's play area and the player's hand, which both have a collection of jack cards and a collection of order cards.
        // public void Initialize<T, U, V>(T card, ICardCollection<U> source, ICardCollection<V> destination ) where T:U,V where U : CardModelBase where V:CardModelBase


        public Move(T card, ICardCollection<T> source, Func<ICardCollection<T>> destinationFunction)
        {
            Card = card;
            Source = source;
            _destinationFunction = destinationFunction;
        }

        public T Card { get; }

        public ICardCollection<T> Destination
        {
            get
            {
                if (_destination == null && _destinationFunction != null)
                {
                    _destination = _destinationFunction();
                }
                return _destination;
            }
        }

        public ICardCollection<T> Source { get; }

        public bool Perform()
        {
            bool success;
            success = Source.Remove(Card);
            if (success)
            {
                _destination.Add(Card);
            }
            return success;
        }

        //public IEnumerator<IMove<CardModelBase>> GetEnumerator()
        //{
        //    yield return this;
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public IEnumerator<IMove<CardModelBase>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            Move<T> move2 = obj as Move<T>;
            if (move2 == null)
            {
                return false;
            }
            if (move2 == this)
            {
                return true;
            }
            bool sameCard = move2.Card == Card;
            bool sameDest = move2.Destination == Destination;
            return sameCard && sameDest;
        }

        public override int GetHashCode()
        {
            // adopted from hash for tuples
            const int primeHash = 397;
            unchecked // prevent overflows
            {
                int result = Card.GetHashCode();
                result = (result*primeHash) ^ Destination.GetHashCode();
                return result;
            }
        }
    }
}