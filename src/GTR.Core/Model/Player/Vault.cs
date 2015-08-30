#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Vault : ObservableCardCollection<OrderCardModel>, IBoundable, IConditionalAddable<OrderCardModel>
    {
        public Vault(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public Vault() : base()
        {
            
        }

        public int Capacity { get; set; }
        public bool CanAdd(OrderCardModel card)
        {
            return this.Count < Capacity;
        }
    }
}