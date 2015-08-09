using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Model;
using GTR.Core.Util;

namespace GTR.Core.ViewModel
{
    public class GameViewModel : ObservableObject
    {
        private GameTable    gameTable;

        public GameTable GameTable
        {
            get { return gameTable; }
            set { gameTable = value; RaisePropertyChanged(); }
        }

    }
}
