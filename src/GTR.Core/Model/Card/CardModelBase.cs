#region

using System.Threading;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model
{
    public abstract class CardModelBase : ObservableModel
    {
        protected CardModelBase()
        {
        }

        public abstract string Name { get; }
        public int Id { get; set; }

        public abstract CardSerialization ToDto();
    }
}