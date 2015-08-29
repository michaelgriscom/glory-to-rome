#region

using GTR.Core.Game;

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
    }
}