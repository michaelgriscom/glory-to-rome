#region

using System.Collections.Generic;
using System.Threading;
using GTR.Core.Serialization;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public abstract class CardModelBase : ObservableModel
    {
        public abstract string Name { get; }

        public int Id { get; private set; }

        static int nextId;

        protected CardModelBase()
        {
           Id = Interlocked.Increment(ref nextId);
        }
    }
}