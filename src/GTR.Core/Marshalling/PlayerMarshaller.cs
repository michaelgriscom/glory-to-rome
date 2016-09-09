#region

using System.Collections.Generic;
using System.Linq;
using GTR.Core.Engine;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Marshalling
{
    internal class PlayerMarshaller : IMarshaller<Player, PlayerDto>
    {
        private readonly CardLocationMarshaller<BuildingSite> _buildingSiteClMarshaller;
        private readonly CardLocationMarshaller<JackCardModel> _jackCardMarshaller;
        private readonly CardLocationMarshaller<OrderCardModel> _orderCardMarshaller;

        public PlayerMarshaller(
            CardLocationMarshaller<OrderCardModel> orderCardMarshaller,
            CardLocationMarshaller<JackCardModel> jackCardMarshaller,
            CardLocationMarshaller<BuildingSite> buildingSiteCLMarshaller)
        {
            _orderCardMarshaller = orderCardMarshaller;
            _jackCardMarshaller = jackCardMarshaller;
            _buildingSiteClMarshaller = buildingSiteCLMarshaller;
        }

        public PlayerDto Marshall(Player poco)
        {
            string id = poco.Id;
            List<CardLocationDto> cardLocations = new List<CardLocationDto>();
            var playerJackHand = _jackCardMarshaller.Marshall(poco.Hand.JackCards);
            playerJackHand.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.Hand,
                PlayerId = id,
                Scope = LocationScope.Player,
                CardType = CardType.Jack
            };
            cardLocations.Add(playerJackHand);

            var playerOrderHand = _orderCardMarshaller.Marshall(poco.Hand.OrderCards);
            playerOrderHand.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.Hand,
                PlayerId = id,
                Scope = LocationScope.Player,
                CardType = CardType.Order
            };
            cardLocations.Add(playerOrderHand);

            var playerDemandArea = _orderCardMarshaller.Marshall(poco.DemandArea);
            playerDemandArea.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.DemandArea,
                PlayerId = id,
                Scope = LocationScope.Player,
                CardType = CardType.Order
            };
            cardLocations.Add(playerDemandArea);

            AddCamp(poco, cardLocations);

            var playAreaJack = _jackCardMarshaller.Marshall(poco.PlayArea.JackCards);
            playAreaJack.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.PlayArea,
                PlayerId = id,
                Scope = LocationScope.Global,
                CardType = CardType.Jack
            };
            cardLocations.Add(playAreaJack);

            var playAreaOrder = _orderCardMarshaller.Marshall(poco.PlayArea.OrderCards);
            playAreaOrder.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.PlayArea,
                PlayerId = id,
                Scope = LocationScope.Global,
                CardType = CardType.Order
            };
            cardLocations.Add(playAreaOrder);

            var completedBuildings = _orderCardMarshaller.Marshall(poco.CompletedBuildings);
            completedBuildings.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.CompletedBuildings,
                PlayerId = id,
                Scope = LocationScope.Global,
                CardType = CardType.Order
            };
            cardLocations.Add(completedBuildings);

            var playerSerialization = new PlayerDto
            {
                Id = id,
                CardLocations = cardLocations.ToArray()
            };

            return playerSerialization;
        }

        public Player UnMarshall(PlayerDto slimRepresentation)
        {
            string playerId = slimRepresentation.Id;
            var player = new Player(playerId);

            var vaultDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.Vault);
            if (vaultDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(vaultDto);
                player.Camp.Vault = new Vault(cl);
            }

            var stockPileDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.Stockpile);
            if (stockPileDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(stockPileDto);
                player.Camp.Stockpile = new Stockpile(cl);
            }

            var handOrderDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.Hand && cl.LocationKind.CardType == CardType.Order);
            if (handOrderDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(handOrderDto);
                player.Hand.OrderCards = new Hand.OrderCardGroup(player.Hand, cl);
            }

            var handJackDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.Hand && cl.LocationKind.CardType == CardType.Jack);
            if (handJackDto != null)
            {
                var cl = _jackCardMarshaller.UnMarshall(handJackDto);
                player.Hand.JackCards = new Hand.JackCardGroup(player.Hand, cl);
            }

            var clienteleDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.Clientele);
            if (clienteleDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(clienteleDto);
                player.Camp.Clientele = new Clientele(cl);
            }

            var cbDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.CompletedBuildings);
            if (cbDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(cbDto);
                player.CompletedBuildings = new CompletedBuildings(cl);
            }

            var demandAreaDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.DemandArea);
            if (demandAreaDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(demandAreaDto);
                player.DemandArea = new DemandArea(cl);
            }

            var playAreaOrderDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.PlayArea && cl.LocationKind.CardType == CardType.Order);
            if (playAreaOrderDto != null)
            {
                var cl = _orderCardMarshaller.UnMarshall(playAreaOrderDto);
                player.PlayArea.OrderCards = new PlayArea.OrderCardGroup(player.PlayArea, cl);
            }

            var playAreaJackDto = slimRepresentation.CardLocations?.First(cl => cl.LocationKind.Kind == CardLocationKind.PlayArea && cl.LocationKind.CardType == CardType.Jack);
            if (playAreaJackDto != null)
            {
                var cl = _jackCardMarshaller.UnMarshall(playAreaJackDto);
                player.PlayArea.JackCards = new PlayArea.JackCardGroup(player.PlayArea, cl);
            }

            return player;
        }

        private void AddCamp(Player poco, List<CardLocationDto> cardLocations)
        {
            string id = poco.Id;

            var vault = _orderCardMarshaller.Marshall(poco.Camp.Vault);
            vault.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.Vault,
                PlayerId = id,
                Scope = LocationScope.Private
            };
            cardLocations.Add(vault);

            var stockpile = _orderCardMarshaller.Marshall(poco.Camp.Stockpile);
            stockpile.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.Stockpile,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(stockpile);

            var clientele = _orderCardMarshaller.Marshall(poco.Camp.Clientele);
            clientele.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.Clientele,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(clientele);
        }
    }
}