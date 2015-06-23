#region

using System.Linq;
using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class BuildingFoundation : BoundedSourceTarget<OrderCardModel>
    {
        private string _overridenName;

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
        }

        public override string LocationName
        {
            get
            {
                if (_overridenName != null)
                {
                    return _overridenName;
                }
                if (Items.Count == 0)
                {
                    return "Empty foundation";
                }
                return string.Concat("Foundation for ", Building.Name);
            }
            set { _overridenName = value; }
        }
    }
}