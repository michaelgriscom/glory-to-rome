#region

using System.Linq;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Marshalling
{
    public sealed class CardLocationMarshaller<T, T2> : IMarshaller<ICardCollection<T>, CardLocationDto>
        where T : CardModelBase where T2 : CardSerialization
    {
        private readonly IMarshaller<T, T2> _cardMarshaller;

        public CardLocationMarshaller(IMarshaller<T, T2> cardMarshaller)
        {
            _cardMarshaller = cardMarshaller;
        }

        public CardLocationDto Marshall(ICardCollection<T> poco)
        {
            var dto = new CardLocationDto
            {
                Cards = poco.Select(cPoco => _cardMarshaller.Marshall(cPoco)).ToArray(),
                Id = poco.Id
            };
            return dto;
        }

        public ICardCollection<T> UnMarshall(CardLocationDto dto)
        {
            var cards = dto.Cards.Select(cDto => _cardMarshaller.UnMarshall((T2)cDto));
            var poco = new ObservableCardCollection<T>(cards)
            {
                Id = dto.Id
            };
            return poco;
        }
    }
}