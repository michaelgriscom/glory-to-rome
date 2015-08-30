#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model.Table
{
    public class BuildingFoundation : ObservableCardCollection<OrderCardModel>
    {
        public BuildingFoundation()
        {
            this.Capacity = 1;
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

        private int _capacity;

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; RaisePropertyChanged(); }
        }

    }
}