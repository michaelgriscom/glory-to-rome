#region

using GTR.Core.CardCollections;
using GTR.Core.Serialization;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class PlayArea : ObservableModel
    {
        private JackCardGroup _jackCards;
        private OrderCardGroup _orderCards;

        public PlayArea(string player = "")
        {
            string locationName = string.Format("Player {0} PlayArea", player);
            JackCards = new JackCardGroup(this, locationName);
            OrderCards = new OrderCardGroup(this, locationName);
        }

        public JackCardGroup JackCards
        {
            get { return _jackCards; }
            set
            {
                _jackCards = value;
                RaisePropertyChanged();
            }
        }

        public OrderCardGroup OrderCards
        {
            get { return _orderCards; }
            set
            {
                _orderCards = value;
                RaisePropertyChanged();
            }
        }

        public class JackCardGroup : CardSourceTarget<JackCardModel>
        {
            private readonly PlayArea _playArea;

            public JackCardGroup(PlayArea playArea, string locationName)
                : base(locationName)
            {
                _playArea = playArea;
            }
        }

        public class OrderCardGroup : BoundedCardTarget<OrderCardModel>, ICardSource<OrderCardModel>
        {
            private readonly PlayArea _playArea;

            internal OrderCardGroup(PlayArea PlayArea, string locationName) : base(locationName)
            {
                _playArea = PlayArea;
            }

            public OrderCardModel ElementAt(int index)
            {
                return this[index];
            }
        }
    }
}