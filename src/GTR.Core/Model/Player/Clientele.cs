#region

using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model.CardCollections;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class Clientele : ObservableCardCollection<OrderCardModel>, IConditionalAddable<OrderCardModel>
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

        private int _capacity;

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; RaisePropertyChanged();}
        }

        public bool CanAdd(OrderCardModel card)
        {
            return this.Count < Capacity;
        }
    }
}