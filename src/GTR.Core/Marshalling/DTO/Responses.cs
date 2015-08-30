namespace GTR.Core.Serialization
{
    public class GetReponseSerialization<T> : ResponseSerialization
    {
        public T Serialization;
    }

    public class GameStateResponseSerialization : GetReponseSerialization<GameDto>
    {
    }

    public class MoveHistoryResponseSerialization : GetReponseSerialization<MoveHistorySerialization>
    {
    }

    public class MoveResponseSerialization : ResponseSerialization
    {
    }

    public class ResponseSerialization
    {
        public string Message;
        public bool Success;
    }
}