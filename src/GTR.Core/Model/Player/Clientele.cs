#region

using GTR.Core.Game;
using GTR.Core.Model.CardCollections;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class Clientele : ObservableCardCollection<OrderCardModel>
    {
        private int _capacity;

        public Clientele()
        {
            CanFollow = new WrappedFunc<OrderCardModel, RoleType, bool>(CanFollowBase);
            Capacity = 2;
        }

        public Clientele(ICardCollection<OrderCardModel> cards) : base(cards)
        {
        }

        internal WrappedFunc<OrderCardModel, RoleType, bool> CanFollow { get; private set; }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                RaisePropertyChanged();
            }
        }

        private bool CanFollowBase(OrderCardModel client, RoleType role)
        {
            return client.RoleType == role;
        }

        public bool CanAdd(OrderCardModel card)
        {
            return Count < Capacity;
        }
    }
}