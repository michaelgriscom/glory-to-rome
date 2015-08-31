#region

using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Services
{
    public class StartGameRequest : Request
    {
        public string GameId;
    }

    public class StartGameResponseSerialization : ResponseSerialization
    {
        public GameDto Game;
    }

    public class CreateGameRequest : Request
    {
        public GameOptions GameOptions;
    }

    public class GameInfoResponseSerialization : ResponseSerialization
    {
        public GameDto Game;
    }
}