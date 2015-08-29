#region

using System.Threading;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model
{
    public abstract class CardModelBase : ObservableModel
    {
        private static int nextId;

        protected CardModelBase()
        {
            Id = Interlocked.Increment(ref nextId);
        }

        public abstract string Name { get; }
        public int Id { get; private set; }
    }
}