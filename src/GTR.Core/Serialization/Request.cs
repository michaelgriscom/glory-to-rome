namespace GTR.Core.Serialization
{
    public class Request
    {
        public int AuthorizationToken;
    }

    public class GameStateRequest : Request
    {
        public int GameId;
    }

    public class MoveHistoryRequest : Request
    {
        public int GameId;
    }

    public class MoveSetRequest : Request
    {
        public int GameId;

        public MoveSetSerialization MoveSet;
    }
}