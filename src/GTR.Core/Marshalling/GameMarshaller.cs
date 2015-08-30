#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GTR.Core.Engine;
using GTR.Core.Marshalling;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Serialization
{
    public class GameMarshaller : IMarshaller<Model.Game, GameDto>
    {
        PlayerMarshaller playerMarshaller = new PlayerMarshaller();
        CardLocationMarshaller cardLocationMarshaller = new CardLocationMarshaller();

        public GameDto Marshall(Model.Game poco)
        {
            GameDto dto = new GameDto();

            var fatRepPlayers = poco.GameTable.Players;

            dto.Players = fatRepPlayers.Select(p => playerMarshaller.Marshall(p)).ToArray();

            var cardLocDtos = new List<CardLocationDto>();

            var orderDeckDto = cardLocationMarshaller.Marshall(poco.GameTable.OrderDeck);
            orderDeckDto.LocationKind = new CardLocationKindSerialization()
            {
                Scope = LocationScope.Global,
                Kind = CardLocationKind.OrderDeck
            };
            cardLocDtos.Add(orderDeckDto);

            var jackDeckDto = cardLocationMarshaller.Marshall(poco.GameTable.JackDeck);
            jackDeckDto.LocationKind = new CardLocationKindSerialization()
            {
                Scope = LocationScope.Global,
                Kind = CardLocationKind.JackDeck
            };
            cardLocDtos.Add(jackDeckDto);

            dto.Id = poco.Id;
            return dto;
        }

        public Model.Game UnMarshall(GameDto dto)
        {
            Model.Game poco = new Model.Game();

            poco.Id = dto.Id;
            var orderDeckDto = dto.CardLocations.First(cl => cl.LocationKind.Kind == CardLocationKind.OrderDeck);
            if (orderDeckDto != null)
            {
                poco.GameTable.OrderDeck = (OrderDeck)cardLocationMarshaller.UnMarshall(orderDeckDto);
            }

            var jackDeckDto = dto.CardLocations.First(cl => cl.LocationKind.Kind == CardLocationKind.JackDeck);
            if (jackDeckDto != null)
            {
                poco.GameTable.JackDeck = (JackDeck)cardLocationMarshaller.UnMarshall(jackDeckDto);
            }

            var players = dto.Players.Select(p => playerMarshaller.UnMarshall(p));
            poco.GameTable.Players = new ObservableCollection<Player>(players);

            return poco;
        }
    }
}