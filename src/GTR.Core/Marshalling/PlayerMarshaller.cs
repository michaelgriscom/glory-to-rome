#region

using System.Collections.Generic;
using GTR.Core.Engine;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;

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
                Scope = LocationScope.Player
            };
            cardLocations.Add(playerJackHand);

            var playerOrderHand = _orderCardMarshaller.Marshall(poco.Hand.OrderCards);
            playerOrderHand.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.Hand,
                PlayerId = id,
                Scope = LocationScope.Player
            };
            cardLocations.Add(playerOrderHand);

            var playerDemandArea = _orderCardMarshaller.Marshall(poco.DemandArea);
            playerDemandArea.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.DemandArea,
                PlayerId = id,
                Scope = LocationScope.Player
            };
            cardLocations.Add(playerDemandArea);

            AddCamp(poco, cardLocations);

            var playAreaJack = _jackCardMarshaller.Marshall(poco.PlayArea.JackCards);
            playAreaJack.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.PlayArea,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(playAreaJack);

            var playAreaOrder = _orderCardMarshaller.Marshall(poco.PlayArea.OrderCards);
            playAreaOrder.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.PlayArea,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(playAreaOrder);

            var completedBuildings = _orderCardMarshaller.Marshall(poco.CompletedBuildings);
            completedBuildings.LocationKind = new CardLocationKindSerialization
            {
                Kind = CardLocationKind.CompletedBuildings,
                PlayerId = id,
                Scope = LocationScope.Global
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