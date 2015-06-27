using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GTR.Core.Game;
using GTR.Core.Model;

namespace GTR.Windows.Design
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
