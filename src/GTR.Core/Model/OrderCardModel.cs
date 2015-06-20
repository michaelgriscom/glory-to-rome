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

        public RoleType RoleType { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }
    }
}