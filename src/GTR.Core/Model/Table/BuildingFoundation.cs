#region

using System.Linq;
using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class BuildingFoundation : BoundedCardCollection<OrderCardModel>
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
    }
}