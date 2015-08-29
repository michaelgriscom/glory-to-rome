﻿#region

using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Action
{
    // consider: do we want movespace to be a generic?
    public class MoveSpace : MoveSpaceBase<IAction>
    {
        public MoveSpace(bool isRequired = false) : base(isRequired)
        {
        }
    }

    public class MoveSet : List<Move<CardModelBase>>, IAction
    {
        public IEnumerator<IMove<CardModelBase>> GetEnumerator()
        {
            return base.GetEnumerator();
        }

        public bool Perform()
        {
            bool success = TrueForAll(move => move.Perform());
            return success;
        }
    }

    public class MoveSpaceBase<T> : IEnumerable<T> where T : IAction
    {
        private readonly HashSet<T> _actions;

        public MoveSpaceBase(bool isRequired = false)
        {
            IsRequired = isRequired;
            _actions = new HashSet<T>();
        }

        public bool IsRequired { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return _actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Add(T action)
        {
            _actions.Add(action);
            return false;
        }
    }
}