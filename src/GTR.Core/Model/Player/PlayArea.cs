﻿#region

using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class PlayArea : ObservableModel
    {
        private JackCardGroup _jackCards;
        private OrderCardGroup _orderCards;

        public PlayArea(string player = "")
        {
            JackCards = new JackCardGroup(this);
            OrderCards = new OrderCardGroup(this);
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

        public class JackCardGroup : ObservableCardCollection<JackCardModel>
        {
            private readonly PlayArea _playArea;

            public JackCardGroup(PlayArea playArea)
            {
                _playArea = playArea;
            }
        }

        public class OrderCardGroup : ObservableCardCollection<OrderCardModel>
        {
            private readonly PlayArea _playArea;

            internal OrderCardGroup(PlayArea PlayArea)
            {
                _playArea = PlayArea;
            }
        }
    }
}