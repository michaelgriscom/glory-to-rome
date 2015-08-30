#region

using GTR.Core.CardCollections;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Vault : ObservableCardCollection<OrderCardModel>
    {
        public Vault(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public Vault()
        {
            
        }

        private int _capacity;

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; RaisePropertyChanged(); }
        }

        public bool CanAdd(OrderCardModel card)
        {
            return this.Count < Capacity;
        }
    }
}