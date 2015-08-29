namespace GTR.Core.Serialization
{
    public class CreateGameResponseSerialization : ResponseSerialization
    {
        public string GameId;
    }

    public class JoinGameResponseSerialization : ResponseSerialization
    {
        public int PlayerId;
    }

    public class ListGamesResponseSerialization : ResponseSerialization
    {
        public LobbyGameSerialization[] Games;
    }

    public class LobbyGameSerialization : ResponseSerialization
    {
        public string GameId;
        public int[] PlayerIds;
    }

    public class JoinGameRequest : Request
    {
        public string GameId;
    }

}