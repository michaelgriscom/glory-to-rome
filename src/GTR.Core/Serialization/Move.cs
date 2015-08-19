namespace GTR.Core.Serialization
{
    public class MoveSerialization
    {
        public int CardId;
        public int DestinationId;
        public int SourceId;
    }

    public class ExecutedMoveSerialization
    {
        public MoveSerialization Move;
        public int PlayerId;
    }


    public class MoveSetSerialization
    {
        public MoveSerialization[] Moves;
    }

    public class MoveSpaceSerialization
    {
        public bool IsRequired;
        public MoveSetSerialization[] MoveSet;
    }
}