#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Vault : ObservableCardCollection<OrderCardModel>, IBoundable
    {
        public Vault(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public Vault() : base()
        {
            
        }

        public int Capacity { get; set; }
    }
}