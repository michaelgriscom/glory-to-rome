#region

using System;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Moves;

#endregion

namespace GTR.Core.Marshalling
{
    public class MoveMarshaller<T> : IMarshaller<Move<T>, MoveSerialization> where T : CardModelBase
    {
        private Model.Game game;
        private ICardLocator cardLocator;
        private ICardCollectionLocator collectionLocator;

        public MoveMarshaller(ICardLocator cardLocator, ICardCollectionLocator collectionLocator)
        {
            this.cardLocator = cardLocator;
            this.collectionLocator = collectionLocator;
        }

        public Move<T> UnMarshall(MoveSerialization slimRepresentation)
        {
            var card = cardLocator.Locate<T>(slimRepresentation.CardId);
            var source = collectionLocator.Locate<T>(slimRepresentation.SourceId);
            var destination = collectionLocator.Locate<T>(slimRepresentation.DestinationId);
            var move = new Move<T>(card, source, destination);
            return move;
        }

        public MoveSerialization Marshall(Move<T> poco)
        {
            return new MoveSerialization()
            {
                CardId = poco.Card.Id,
                DestinationId = poco.Destination.Id,
                SourceId = poco.Source.Id
            };
        }
    }
}