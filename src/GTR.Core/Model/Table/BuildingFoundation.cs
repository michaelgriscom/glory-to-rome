#region

using System.Linq;
using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class BuildingFoundation : BoundedSourceTarget<OrderCardModel>
    {
        public BuildingFoundation() : base(1)
        {
        }

        public OrderCardModel Building
        {
            get
            {
                if (Items.Count == 0)
                {
                    return null;
                }
                return Items.ElementAt(0);
            }
            set
            {
                if (Items.Count == 0)
                {
                    Items.Add(value);
                }
                else
                {
                    Items[0] = value;
                }
            }
        }
    }
}