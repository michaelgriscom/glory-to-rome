namespace GTR.Core.Marshalling.DTO
{
    public class MoveSerialization : IDto
    {
        public string Id;
        public int CardId;
        public string DestinationId;
        public string SourceId;
    }

    public class ExecutedMoveSerialization : IDto
    {
        public MoveSerialization Move;
        public int PlayerId;
    }


    public class MoveSetSerialization : IDto
    {
        public MoveSerialization[] Moves;
    }

    public class MoveSpaceSerialization : IDto
    {
        public bool IsRequired;
        public MoveSetSerialization[] MoveSet;
    }
}