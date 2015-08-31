#region

using System;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Moves;

#endregion

namespace GTR.Core.Marshalling
{
    public class MoveMarshaller : IMarshaller<Move<CardModelBase>, MoveSerialization>
    {
        private Model.Game game;

        public MoveMarshaller(Model.Game game)
        {
            this.game = game;
        }

        public Move<CardModelBase> UnMarshall(MoveSerialization slimRepresentation)
        {
            throw new NotImplementedException();
        }

        public MoveSerialization Marshall(Move<CardModelBase> poco)
        {
            throw new NotImplementedException();
        }
    }
}