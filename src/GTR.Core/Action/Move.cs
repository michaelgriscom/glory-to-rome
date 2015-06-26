#region

using System;
using System.Collections;
using System.Collections.Generic;
using GTR.Core.CardCollections;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Action
{
    public class Move<T> : IMove<T> where T : CardModelBase
    {
        private readonly T _card;
        private readonly Func<ICardTarget<T>> _destinationFunction;
        private readonly ICardSource<T> _source;
        // this member should be treated as readonly after its initial assignment,
        // however we want to allow it to be evaluated lazily through a Func, so
        // we have to rely on this class not to mutate it after evaluation. Otherwise,
        // the hashcode could change and we could lose the object in a collection.
        private ICardTarget<T> _destination;

        public Move(T card, ICardSource<T> source, ICardTarget<T> destination)
        {
            _card = card;
            _source = source;
            _destination = destination;
        }

        // consider: the constructor is pickier than it should be.
        // As long as source<U> destination<U> and T card have the relationship T:U
        // then it's a valid move. The problem is that C# doesn't support constraints in a constructor,
        // so we'd need a messy Initialize method with a signature like below. The problem though is that we'd have to expose U and V
        // through the getters, so this type will become really ugly. Instead, the ugliness is currently isolated to two main places,
        // the player's play area and the player's hand, which both have a collection of jack cards and a collection of order cards.
        // public void Initialize<T, U, V>(T card, ICardSource<U> source, ICardTarget<V> destination ) where T:U,V where U : CardModelBase where V:CardModelBase


        public Move(T card, ICardSource<T> source, Func<ICardTarget<T>> destinationFunction)
        {
            _card = card;
            _source = source;
            _destinationFunction = destinationFunction;
        }

        public T Card
        {
            get { return _card; }
        }

        public ICardLocation<T> Destination
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

        public ICardLocation<T> Source
        {
            get { return _source; }
        }

        public bool Perform()
        {
            bool success;
            success = _source.Remove(_card);
            if (success)
            {
                _destination.Add(_card);
                // TODO: add in dependency injection or something to avoid doing this
                Game.Game.MessageProvider.Display(string.Format("Card [{0}] moved from [{1}] to [{2}]", _card.Name,
                    _source.LocationName, _destination.LocationName));
            }
            return success;
        }

        public IEnumerator<IMove<CardModelBase>> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            bool sameCard = move2._card == _card;
            bool sameDest = move2.Destination == Destination;
            return sameCard && sameDest;
        }

        public override int GetHashCode()
        {
            // adopted from hash for tuples
            const int primeHash = 397;
            unchecked // prevent overflows
            {
                int result = _card.GetHashCode();
                result = (result*primeHash) ^ Destination.GetHashCode();
                return result;
            }
        }
    }
}