namespace GTR.Core.Serialization
{
    public class GameStateSerialization
    {
        public CardLocationSerialization[] CardLocations;
        public PlayerSerialization[] Players;
    }

    public class MoveHistorySerialization
    {
        public GameStateSerialization InitialState;
        public ExecutedMoveSerialization[] Moves;
    }


    public class PlayerSerialization
    {
        public int Id;
        public string Name;
    }
}