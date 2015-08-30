using System;
using System.Linq;
using GTR.Core.CardCollections;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling
{
    class CardLocationMarshaller : IMarshaller<ICardLocation<CardModelBase>, CardLocationDto>
    {
        private CardMarshaller cardMarshaller;

        public CardLocationMarshaller(CardMarshaller cardMarshaller)
        {
            this.cardMarshaller = cardMarshaller;
        }

        public CardLocationDto Marshall(ICardLocation<CardModelBase> poco)
        {
            var dto = new CardLocationDto();
            dto.Cards = poco.Select(c => cardMarshaller.Marshall(c)).ToArray();
            dto.Id = poco.Id;
            return dto;
        }

        public ICardLocation<CardModelBase> UnMarshall(CardLocationDto slimRepresentation)
        {
            throw new NotImplementedException();
        }
    }
}