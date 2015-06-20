#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GTR.Core.Game
{
    // ReSharper disable once InconsistentNaming
    public class WrappedFunc<T, U>
    {
        private readonly Func<T, U> _baseFunc;
        private readonly ISet<Func<T, U, U>> _funcWrappers;

        public WrappedFunc(Func<T, U> baseFunc)
        {
            _baseFunc = baseFunc;
            _funcWrappers = new HashSet<Func<T, U, U>>();
        }

        public U Execute(T originalParam)
        {
            var runningResult = _baseFunc(originalParam);
            return _funcWrappers.Aggregate(runningResult, (current, funcWrapper) => funcWrapper(originalParam, current));
        }

        public void Unwrap(Func<T, U, U> funcWrapper)
        {
            _funcWrappers.Remove(funcWrapper);
        }

        public void Wrap(Func<T, U, U> funcWrapper)
        {
            _funcWrappers.Add(funcWrapper);
        }
    }
}

namespace GTR.Core
{
    // ReSharper disable once InconsistentNaming
    public class WrappedFunc<U>
    {
        private readonly Func<U> _baseFunc;
        private readonly ISet<Func<U, U>> _funcWrappers;

        public WrappedFunc(Func<U> baseFunc)
        {
            _baseFunc = baseFunc;
            _funcWrappers = new HashSet<Func<U, U>>();
        }

        public U Execute()
        {
            var runningResult = _baseFunc();
            if (runningResult == null) throw new ArgumentNullException("runningResult");
            return _funcWrappers.Aggregate(runningResult, (current, funcWrapper) => funcWrapper(current));
        }

        public void Unwrap(Func<U, U> funcWrapper)
        {
            _funcWrappers.Remove(funcWrapper);
        }

        public void Wrap(Func<U, U> funcWrapper)
        {
            _funcWrappers.Add(funcWrapper);
        }
    }
}

namespace GTR.Core.Game
{
    // ReSharper disable once InconsistentNaming
    internal class WrappedFunc<T1, T2, U>
    {
        private readonly Func<T1, T2, U> _baseFunc;
        private readonly ISet<Func<T1, T2, U, U>> _funcWrappers;

        public WrappedFunc(Func<T1, T2, U> baseFunc)
        {
            _baseFunc = baseFunc;
            _funcWrappers = new HashSet<Func<T1, T2, U, U>>();
        }

        public U Execute(T1 originalParam1, T2 originalParam2)
        {
            var runningResult = _baseFunc(originalParam1, originalParam2);
            return _funcWrappers.Aggregate(runningResult,
                (current, funcWrapper) => funcWrapper(originalParam1, originalParam2, current));
        }

        public void Unwrap(Func<T1, T2, U, U> funcWrapper)
        {
            _funcWrappers.Remove(funcWrapper);
        }

        public void Wrap(Func<T1, T2, U, U> funcWrapper)
        {
            _funcWrappers.Add(funcWrapper);
        }
    }
}