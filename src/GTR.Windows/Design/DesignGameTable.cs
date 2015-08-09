using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;

namespace GTR.Windows.Design
{
    public class DesignGameTable : GameTable
    {
        public DesignGameTable() : base(new OrderDeck(), new JackDeck())
        {
            this.Players = new ObservableCollection<Player>() {new DesignPlayer()};
        }
    }
}
