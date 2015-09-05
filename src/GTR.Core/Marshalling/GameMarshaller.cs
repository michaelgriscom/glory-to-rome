#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GTR.Core.DeckManagement;
using GTR.Core.Engine;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Services;

#endregion

namespace GTR.Core.Marshalling
{
    public class GameMarshaller : IMarshaller<Model.Game, GameDto>
    {
        private readonly CardLocationMarshaller<BuildingSite, BuildingFoundationSerialization> buildingSiteCLMarshaller;
        private readonly CardLocationMarshaller<JackCardModel, JackCardSerialization> jackCLMarshaller;
        private readonly CardLocationMarshaller<OrderCardModel, OrderCardSerialization> orderCLMarshaller;
        private readonly PlayerMarshaller playerMarshaller;

        public GameMarshaller(IDeckIo deckIo, IResourceProvider resourceProvider)
        {
            var cardManager = new CardManager(resourceProvider, deckIo);

            var orderCardMarshaller = new OrderCardMarshaller(cardManager.CardSet);
            orderCLMarshaller = new CardLocationMarshaller<OrderCardModel, OrderCardSerialization>(orderCardMarshaller);

            var jackCardMarshaller = new JackCardMarshaller();
            jackCLMarshaller = new CardLocationMarshaller<JackCardModel, JackCardSerialization>(jackCardMarshaller);

            var buildingSiteMarshaller = new BuildSiteMarshaller();
            buildingSiteCLMarshaller = new CardLocationMarshaller<BuildingSite, BuildingFoundationSerialization>(buildingSiteMarshaller);

            playerMarshaller = new PlayerMarshaller(orderCLMarshaller, jackCLMarshaller, buildingSiteCLMarshaller);
        }

        public GameDto Marshall(Model.Game poco)
        {
            if (poco == null)
            {
                return null;
            }

            var playerPocos = poco.GameTable.Players;

            var playerDtos = playerPocos.Select(p => playerMarshaller.Marshall(p)).ToArray();

            var cardLocDtos = new List<CardLocationDto>();

            var orderDeckDto = orderCLMarshaller.Marshall(poco.GameTable.OrderDeck);
            orderDeckDto.LocationKind = new CardLocationKindSerialization
            {
                Scope = LocationScope.Global,
                Kind = CardLocationKind.OrderDeck
            };
            cardLocDtos.Add(orderDeckDto);

            var jackDeckDto = jackCLMarshaller.Marshall(poco.GameTable.JackDeck);
            jackDeckDto.LocationKind = new CardLocationKindSerialization
            {
                Scope = LocationScope.Global,
                Kind = CardLocationKind.JackDeck
            };
            cardLocDtos.Add(jackDeckDto);

            GameDto dto = new GameDto
            {
                Players = playerDtos,
                Id = poco.Id,
                CardLocations = cardLocDtos.ToArray(),
                GameOptions = poco.GameOptions
            };
            return dto;
        }

        public Model.Game UnMarshall(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            Model.Game poco = new Model.Game();
            poco.GameTable = new GameTable();
            poco.GameOptions = dto.GameOptions;

            poco.Id = dto.Id;
            var orderDeckDto = dto.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.OrderDeck);
            if (orderDeckDto != null)
            {
                var cl = orderCLMarshaller.UnMarshall(orderDeckDto);
                poco.GameTable.OrderDeck = new OrderDeck(cl);
            }

            var jackDeckDto = dto.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.JackDeck);
            if (jackDeckDto != null)
            {
                var cl = jackCLMarshaller.UnMarshall(jackDeckDto);
                poco.GameTable.JackDeck = new JackDeck(cl);
            }

            var players = dto.Players?.Select(p => playerMarshaller.UnMarshall(p));
            poco.GameTable.Players = new ObservableCollection<Player>(players);

            return poco;
        }
    }
}