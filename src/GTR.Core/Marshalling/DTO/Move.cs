namespace GTR.Core.Marshalling.DTO
{
    public class MoveSerialization : IDto
    {
        public int CardId;
        public int DestinationId;
        public int SourceId;
        public CardType CardType;
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