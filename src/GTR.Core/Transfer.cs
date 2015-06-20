using System;
using System.Collections.Generic;

namespace GloryToRome
{
    internal class Transfer : IPlayerAction
    {
        private bool isRequired;
        private Func<Card, IEnumerable<ICardTarget<Card>>> destinationFunction;
        private Func<IEnumerable<Card>> sourceFunction;

        private bool IsRequired { get { return isRequired; } }

        internal Transfer(Func<IEnumerable<Card>> source, Func<Card, IEnumerable<ICardTarget<Card>>> destinationFunction, bool isRequired)
        {
            this.isRequired = isRequired;
            this.sourceFunction = source;
            this.destinationFunction = destinationFunction;
        }

        internal Transfer(Func<IEnumerable<Card>> source, Func<Card, IEnumerable<ICardTarget<Card>>> destinationFunction)
            : this(source, destinationFunction, false)
        {
        }

        // could also be an iterator
        internal IEnumerable<Card> SourceCards()
        {
            return sourceFunction();
        }

        internal IEnumerable<ICardTarget<Card>> DestinationTargets(Card card)
        {
            return destinationFunction(card);
        }
    }
}