using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.CardCollections;
using GTR.Core.Engine;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling
{
    class PlayerMarshaller : IMarshaller<Player, PlayerDto>
    {
        private CardLocationMarshaller cardLocationMarshaller;

        public PlayerDto Marshall(Player poco)
        {
            string id = poco.Id;
            //List<CardLocationDto> cardLocations = new List<CardLocationDto>();
            //var playerJackHand = cardLocationMarshaller.Marshall(poco.Hand.JackCards);
            //var playerOrderHand = cardLocationMarshaller.Marshall(poco.Hand.OrderCards);
            //var playerDemandArea = cardLocationMarshaller.Marshall(poco.DemandArea);


            var playerSerialization = new PlayerDto()
            {
                Id = id
            };

            return playerSerialization;
        }

        public Player UnMarshall(PlayerDto slimRepresentation)
        {
            string playerId = slimRepresentation.Id;
            var player = new Player(playerId);
            return player;
        }
    }

    class CardLocationMarshaller : IMarshaller<ICardLocation<CardModelBase>, CardLocationDto>
    {
        private CardMarshaller cardMarshaller;

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
