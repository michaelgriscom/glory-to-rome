using GTR.Core.Marshalling.DTO;

namespace GTR.Core.Serialization
{
    public class MoveHistorySerialization : IDto
    {
        public GameDto InitialState;
        public ExecutedMoveSerialization[] Moves;
    }

    public class LeaveGameResponseSerialization : ResponseSerialization
    {
    }

    public class LeaveGameRequest : Request
    {
        public int GameId;
    }

    public class EndGameResponseSerialization : ResponseSerialization
    {
    }

    public class EndGameRequest : Request
    {
        public int GameId;
    }
}