using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Engine;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

namespace GTR.Core.Moves
{
    public class MoveMaker
    {
        public event MoveHandler OnMove = delegate {};

        private MoveHistory moveHistory;

        public MoveMaker()
        {
            moveHistory = new MoveHistory();
        }

        public bool MakeMove<T>(T Card,
            ICardCollection<T> Source,
            ICardCollection<T> Destination,
            Player mover) where T : CardModelBase
        {
            bool success = Source.Remove(Card);
            if (!success)
            {
                Debug.Assert(success, "Invalid move.");
            }

            Destination.Add(Card);

            var eventArgs = new MoveEventArgs() { Card = Card.Id, Destination = Destination.Id, Source = Source.Id, Player = mover};
            OnMove(this, eventArgs);
            RecordMove(eventArgs);
            return true;
        }

        private void RecordMove(MoveEventArgs moveEventArgs)
        {
            MoveSerialization serialization = new MoveSerialization()
            {
                CardId = moveEventArgs.Card,
                DestinationId = moveEventArgs.Destination,
                Id = Guid.NewGuid().ToString(),
                SourceId = moveEventArgs.Source
            };

            var playerId = moveEventArgs.Player == null ? "NPC" : moveEventArgs.Player.Id;
            ExecutedMoveSerialization move = new ExecutedMoveSerialization()
            {
                Move = serialization,
                PlayerId = playerId
            };
            moveHistory.AddMove(move);
        }

        internal void MakeMove(IAction move)
        {
            // this method shouldn't be implemented, we want to move away from strongly typed move objects. Instead, replace with MakeMove(move serialization)
            throw new NotImplementedException();
        }

        public void MakeMove<T>(Move<T> move) where T : CardModelBase
        {
            MakeMove(move.Card, move.Source, move.Destination, null);
        }

        public MoveHistory History { get { return moveHistory; } }
    }

    public class MoveHistory
    {
        private ObservableCollection<ExecutedMoveSerialization> _executedMoves;

        public MoveHistory()
        {
            _executedMoves = new ObservableCollection<ExecutedMoveSerialization>();
        }

        public IEnumerable<ExecutedMoveSerialization> Moves { get { return _executedMoves; }  }

        public void AddMove(ExecutedMoveSerialization move)
        {
            _executedMoves.Add(move);
        }
    }

    public delegate void MoveHandler(object sender, MoveEventArgs e);

    public class MoveEventArgs : EventArgs
    {
        public int Card;
        public string Destination;
        public string Source;
        public Player Player;
    }
}
