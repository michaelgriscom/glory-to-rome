using GTR.Core.Game;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling.DTO
{
    public class GameDto : IDto
    {
        public GameOptions GameOptions;
        public CardLocationDto[] CardLocations;
        public PlayerDto[] Players;
        public string Id;
    }
}