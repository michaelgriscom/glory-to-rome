#region

using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model.Table
{
    public class BuildingFoundation : ObservableCardCollection<OrderCardModel>
    {
        private int _capacity;

        public BuildingFoundation()
        {
            Capacity = 1;
        }

        public OrderCardModel Building
        {
            get
            {
                if (Count == 0)
                {
                    return null;
                }
                return ElementAt(0);
            }
            set
            {
                if (Count > 0)
                {
                    RemoveAt(0);
                }
                Add(value);
            }
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
    }
}