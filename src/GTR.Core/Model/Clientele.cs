#region

using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class Clientele : BoundedCardTarget<OrderCardModel>
    {
        private readonly string _locationName;

        public Clientele(string player = "")
        {
            CanFollow = new WrappedFunc<OrderCardModel, RoleType, bool>(CanFollowBase);
            _locationName = string.Format("Player {0} clientele", player);
        }

        internal WrappedFunc<OrderCardModel, RoleType, bool> CanFollow { get; private set; }

        public bool IsFull
        {
            get { return false; // TODO: implement
            }
        }

        public override string LocationName
        {
            get { return _locationName; }
        }

        private bool CanFollowBase(OrderCardModel client, RoleType role)
        {
            return client.RoleType == role;
        }
    }
}