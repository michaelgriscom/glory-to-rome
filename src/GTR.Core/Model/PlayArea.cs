using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.CardCollections;

namespace GTR.Core.Model
{
    public class PlayArea
    {
        internal JackCardGroup JackCards { get; private set; }
        internal OrderCardGroup OrderCards { get; private set; }

        internal PlayArea(string player = "")
        {
            string locationName =  string.Format("Player {0} PlayArea", player);
            JackCards = new JackCardGroup(this, locationName);
            OrderCards = new OrderCardGroup(this, locationName);
        }

        internal class JackCardGroup : CardSourceTarget<JackCardModel>
        {
            private readonly PlayArea _playArea;

            public JackCardGroup(PlayArea playArea, string locationName)
                : base(locationName)
            {
                _playArea = playArea;
            }
        }

        internal class OrderCardGroup : BoundedCardTarget<OrderCardModel>, ICardSource<OrderCardModel>
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
