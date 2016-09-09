#region

using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Universal.Design
{
    public class DesignHand : Hand
    {
        public DesignHand()
        {
            RefillCapacity = 5;
            OrderCards.Add(new OrderCardModel("Palisade", "fasdasdfsdf", RoleType.Craftsman));
            OrderCards.Add(new OrderCardModel("Dock", "fasdasdfsdf", RoleType.Craftsman));
            OrderCards.Add(new OrderCardModel("Bar", "fasdasdfsdf", RoleType.Laborer));
            JackCards.Add(new JackCardModel());
            JackCards.Add(new JackCardModel());
        }
    }
}