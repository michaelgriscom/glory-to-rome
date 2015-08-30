#region

using GTR.Core.Game;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model
{
    public class OrderCardModel : HandCardModel
    {
        public OrderCardModel(string name, string description, RoleType roleType)
        {
            Name = name;
            Description = description;
            RoleType = roleType;
        }

        public OrderCardModel()
        {
        }

        public RoleType RoleType { get; set; }
        public string Description { get; set; }
        public override string Name { get; }

        public override CardSerialization ToDto()
        {
            return new CardSerialization()
            {
                BuildingName = Name,
                CardType = CardType.Order,
                Id = Id,
                Material = this.RoleType.ToMaterial()
            };
        }
    }
}