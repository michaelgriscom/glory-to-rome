using System;
using System.Linq;
using GTR.Core.CardCollections;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling
{
    class OrderCardLocationMarshaller : IMarshaller<ICardCollection<OrderCardModel>, CardLocationDto>
    {
        private CardMarshaller cardMarshaller;

        public OrderCardLocationMarshaller(CardMarshaller cardMarshaller)
        {
            this.cardMarshaller = cardMarshaller;
        }

        public CardLocationDto Marshall(ICardCollection<OrderCardModel> poco)
        {
            var dto = new CardLocationDto();
            dto.Cards = poco.Select(c => cardMarshaller.Marshall(c)).ToArray();
            dto.Id = poco.Id;
            return dto;
        }

        public ICardCollection<OrderCardModel> UnMarshall(CardLocationDto slimRepresentation)
        {
            throw new NotImplementedException();
        }

        public CardLocationDto Marshall(ICardCollection<JackCardModel> poco)
        {
            var dto = new CardLocationDto();
            dto.Cards = poco.Select(c => cardMarshaller.Marshall(c)).ToArray();
            dto.Id = poco.Id;
            return dto;
        }
    }
}