using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;
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

        private Player player;

        public GameViewModel(Game.Game game)
        {
            this.GameTable = game.GameTable;
            MessageProvider = Game.Game.MessageProvider;
        }

        public Player Player
        {
            get { return player; }
            set { player = value; RaisePropertyChanged();}
        }

        private IMessageProvider messageProvider;

        public IMessageProvider MessageProvider
        {
            get { return messageProvider; }
            set { messageProvider = value; RaisePropertyChanged(); }
        }

    }
}
