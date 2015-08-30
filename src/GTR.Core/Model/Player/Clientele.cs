#region

using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class Clientele : ObservableCardCollection<OrderCardModel>, IBoundable, IConditionalAddable<OrderCardModel>
    {
        public Clientele()
        {
            CanFollow = new WrappedFunc<OrderCardModel, RoleType, bool>(CanFollowBase);
            this.Capacity = 2;
        }

        public Clientele(ICardCollection<OrderCardModel> cards) : base(cards)
        {
            
        }

        internal WrappedFunc<OrderCardModel, RoleType, bool> CanFollow { get; private set; }

        private bool CanFollowBase(OrderCardModel client, RoleType role)
        {
            return client.RoleType == role;
        }

        public int Capacity { get; set; }
        public bool CanAdd(OrderCardModel card)
        {
            return this.Count < Capacity;
        }
    }
}