#region

using System;

#endregion

namespace GTR.Core.Serialization
{
    public class GameMarshaller : IMarshaller<Game.Game, GameStateSerialization>
    {
        public GameStateSerialization Marshall(Game.Game fatRepresentation)
        {
            throw new NotImplementedException();
        }

        public Game.Game UnMarshall(GameStateSerialization slimRepresentation)
        {
            throw new NotImplementedException();
        }
    }
}