using System;
using System.Linq;
using GTR.Core.CardCollections;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling
{
    sealed public class CardLocationMarshaller<T> : IMarshaller<ICardCollection<T>, CardLocationDto> where T : CardModelBase
    {
        private IMarshaller<T, CardSerialization> _cardMarshaller;

        public CardLocationMarshaller(IMarshaller<T, CardSerialization> cardMarshaller)
        {
            this._cardMarshaller = cardMarshaller;
        } 

        public CardLocationDto Marshall(ICardCollection<T> poco)
        {
            var dto = new CardLocationDto();
            dto.Cards = poco.Select(cPoco => _cardMarshaller.Marshall(cPoco)).ToArray();
            dto.Id = poco.Id;
            return dto;
        }

        public ICardCollection<T> UnMarshall(CardLocationDto dto)
        {
            var cards = dto.Cards.Select(cDto => _cardMarshaller.UnMarshall(cDto));
            var poco = new ObservableCardCollection<T>(cards);
            poco.Id = dto.Id;
            return poco;
        }
    }
}