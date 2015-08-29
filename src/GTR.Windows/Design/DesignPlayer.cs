using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Engine;
using GTR.Core.Game;

namespace GTR.Windows.Design
{
    public class DesignPlayer : Player
    {
        public DesignPlayer() : base("DesignPlayer", null, null)
        {
            this.Board = new DesignBoard();
        }
    }
}
