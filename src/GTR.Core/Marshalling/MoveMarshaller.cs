﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.Model;

namespace GTR.Core.Serialization
{
    public class MoveMarshaller : IMarshaller<Move<CardModelBase>, MoveSerialization>
    {
        private Game.Game game;

        public MoveMarshaller(Game.Game game)
        {
            this.game = game;
        }
        public Move<CardModelBase> UnMarshall(MoveSerialization slimRepresentation)
        {
            throw new NotImplementedException();
        }

        public MoveSerialization Marshall(Move<CardModelBase> fatRepresentation)
        {
            throw new NotImplementedException();
        }
    }
}