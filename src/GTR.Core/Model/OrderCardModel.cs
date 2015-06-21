#region

using GTR.Core.Game;

#endregion

namespace GTR.Core.Model
{
    public class OrderCardModel : HandCardModel
    {
        private readonly string _name;

        public OrderCardModel(string name, string description, RoleType roleType)
        {
            _name = name;
            Description = description;
            RoleType = roleType;
        }

        public RoleType RoleType { get; private set; }
        public string Description { get; private set; }

        public override string Name
        {
            get { return _name; }
        }
    }
}