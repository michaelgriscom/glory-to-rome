#region

using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class Clientele : BoundedCardTarget<OrderCardModel>
    {
        public Clientele(string player = "")
        {
            CanFollow = new WrappedFunc<OrderCardModel, RoleType, bool>(CanFollowBase);
        }

        internal WrappedFunc<OrderCardModel, RoleType, bool> CanFollow { get; private set; }

        public bool IsFull
        {
            get { return false; // TODO: implement
            }
        }

        private bool CanFollowBase(OrderCardModel client, RoleType role)
        {
            return client.RoleType == role;
        }
    }
}