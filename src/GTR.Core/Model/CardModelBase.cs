#region

using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public abstract class CardModelBase : ObservableObject
    {
        public abstract string Name { get; }
    }
}