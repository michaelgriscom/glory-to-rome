#region

#endregion

namespace GTR.Core.Model
{
    public abstract class CardModelBase : ObservableModel
    {
        public abstract string Name { get; }
        public int Id { get; set; }
    }
}