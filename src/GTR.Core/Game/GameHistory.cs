#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Game
{
    internal class GameHistory : ICollection<Move<CardModelBase>>
    {
        private readonly ICollection<Move<CardModelBase>> _gameMoves;

        internal GameHistory()
        {
            _gameMoves = new LinkedList<Move<CardModelBase>>();
        }

        public int Count
        {
            get { return _gameMoves.Count; }
        }

        public bool IsReadOnly
        {
            get { return _gameMoves.IsReadOnly; }
        }

        public void Add(Move<CardModelBase> move)
        {
            _gameMoves.Add(move);
        }

        public void Clear()
        {
            _gameMoves.Clear();
        }

        public bool Contains(Move<CardModelBase> item)
        {
            return _gameMoves.Contains(item);
        }

        public void CopyTo(Move<CardModelBase>[] array, int arrayIndex)
        {
            _gameMoves.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Move<CardModelBase>> GetEnumerator()
        {
            return _gameMoves.GetEnumerator();
        }

        public bool Remove(Move<CardModelBase> item)
        {
            return _gameMoves.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}