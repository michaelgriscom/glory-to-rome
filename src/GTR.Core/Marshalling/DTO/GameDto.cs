#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.Marshalling.DTO
{
    public class GameDto : IDto
    {
        public CardLocationDto[] CardLocations;
        public GameOptions GameOptions;
        public string Id;
        public PlayerDto[] Players;
    }
}