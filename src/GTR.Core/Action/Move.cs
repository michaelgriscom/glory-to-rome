#region

using System;
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
            }
            return success;
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