using GTR.Core.Serialization;

namespace GTR.Core.Marshalling.DTO
{
    public class PlayerDto : IDto
    {
        public string Id;
        public CardLocationDto[] CardLocations;
    }
}