namespace GTR.Core.Serialization
{
    public class CreateGameResponseSerialization : ResponseSerialization
    {
        public int GameId;
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
        public int GameId;
        public int[] PlayerIds;
    }

    public class JoinGameRequest : Request
    {
        public int GameId;
    }

}