#region

using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Vault : ObservableCardCollection<OrderCardModel>
    {
        private int _capacity;

        public Vault(ICardCollection<OrderCardModel> collection) : base(collection)
        {
        }

        public Vault()
        {
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                RaisePropertyChanged();
            }
        }

        public bool CanAdd(OrderCardModel card)
        {
            return Count < Capacity;
        }
    }
}