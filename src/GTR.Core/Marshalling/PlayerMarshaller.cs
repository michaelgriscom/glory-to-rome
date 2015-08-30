using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Engine;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling
{
    class PlayerMarshaller : IMarshaller<Player, PlayerDto>
    {
        private CardLocationMarshaller cardLocationMarshaller;

        public PlayerMarshaller(CardLocationMarshaller cardLocationMarshaller)
        {
            this.cardLocationMarshaller = cardLocationMarshaller;
        }

        public PlayerDto Marshall(Player poco)
        {
            string id = poco.Id;
            List<CardLocationDto> cardLocations = new List<CardLocationDto>();
            var playerJackHand = cardLocationMarshaller.Marshall(poco.Hand.JackCards);
            playerJackHand.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.Hand,
                PlayerId = id,
                Scope = LocationScope.Player
            };
            cardLocations.Add(playerJackHand);

            var playerOrderHand = cardLocationMarshaller.Marshall(poco.Hand.OrderCards);
            playerOrderHand.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.Hand,
                PlayerId = id,
                Scope = LocationScope.Player
            };
            cardLocations.Add(playerOrderHand);

            var playerDemandArea = cardLocationMarshaller.Marshall(poco.DemandArea);
            playerDemandArea.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.DemandArea,
                PlayerId = id,
                Scope = LocationScope.Player
            };
            cardLocations.Add(playerDemandArea);

            AddCamp(poco, cardLocations);

            var playAreaJack = cardLocationMarshaller.Marshall(poco.PlayArea.JackCards);
            playAreaJack.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.PlayArea,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(playAreaJack);

            var playAreaOrder = cardLocationMarshaller.Marshall(poco.PlayArea.OrderCards);
            playAreaOrder.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.PlayArea,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(playAreaOrder);

            var completedBuildings = cardLocationMarshaller.Marshall(poco.CompletedBuildings);
            completedBuildings.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.CompletedBuildings,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(completedBuildings);

            var playerSerialization = new PlayerDto()
            {
                Id = id,
                CardLocations = cardLocations.ToArray()
            };

            return playerSerialization;
        }

        private void AddCamp(Player poco, List<CardLocationDto> cardLocations)
        {
            string id = poco.Id;

            var vault = cardLocationMarshaller.Marshall(poco.Camp.Vault);
            vault.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.Vault,
                PlayerId = id,
                Scope = LocationScope.Private
            };
            cardLocations.Add(vault);

            var stockpile = cardLocationMarshaller.Marshall(poco.Camp.Stockpile);
            stockpile.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.Stockpile,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(stockpile);

            var clientele = cardLocationMarshaller.Marshall(poco.Camp.Clientele);
            clientele.LocationKind = new CardLocationKindSerialization()
            {
                Kind = CardLocationKind.Clientele,
                PlayerId = id,
                Scope = LocationScope.Global
            };
            cardLocations.Add(clientele);
        }

        public Player UnMarshall(PlayerDto slimRepresentation)
        {
            string playerId = slimRepresentation.Id;
            var player = new Player(playerId);
            return player;
        }
    }
}
