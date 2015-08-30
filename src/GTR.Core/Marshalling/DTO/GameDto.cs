using GTR.Core.Marshalling.DTO;

namespace GTR.Core.Serialization
{
    public class GameDto : IDto
    {
        public CardLocationDto[] CardLocations;
        public PlayerDto[] Players;
        public string Id;
    }
}