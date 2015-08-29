namespace GTR.Core.Serialization
{
    public class GameStateSerialization : IDto
    {
        public CardLocationSerialization[] CardLocations;
        public PlayerSerialization[] Players;
    }

    public class MoveHistorySerialization : IDto
    {
        public GameStateSerialization InitialState;
        public ExecutedMoveSerialization[] Moves;
    }


    public class PlayerSerialization : IDto
    {
        public int Id;
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