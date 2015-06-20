#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Game
{
    public class MoveSpace : IEnumerable<MoveCombo>
    {
        private readonly HashSet<MoveCombo> _moves;

        public MoveSpace(bool isRequired = false)
        {
            IsRequired = isRequired;
            _moves = new HashSet<MoveCombo>();
        }

        public bool IsRequired { get; private set; }

        public IEnumerator<MoveCombo> GetEnumerator()
        {
            return _moves.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// </summary>
        /// <param name="move"></param>
        /// <returns>false if move was already in the space, true otherwise</returns>
        public bool Add(IMove<CardModelBase> move)
        {
            MoveCombo moveSet = new MoveCombo(move);
            Add(moveSet);
            // TODO: how do we handle duplicates?
            return false;
        }

        public bool Add(MoveCombo moveSet)
        {
            _moves.Add(moveSet);
            return false;
        }
    }
}