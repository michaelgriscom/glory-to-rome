#region

using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

#endregion

namespace GTR.Core.ViewModel
{
    public class GameViewModel : ObservableObject
    {
        private GameTable gameTable;
        private IMessageProvider messageProvider;
        private Player player;

        public GameViewModel(Game.Game game)
        {
            GameTable = game.GameTable;
            MessageProvider = Game.Game.MessageProvider;
        }

        public GameTable GameTable
        {
            get { return gameTable; }
            set
            {
                gameTable = value;
                RaisePropertyChanged();
            }
        }

        public Player Player
        {
            get { return player; }
            set
            {
                player = value;
                RaisePropertyChanged();
            }
        }

        public IMessageProvider MessageProvider
        {
            get { return messageProvider; }
            set
            {
                messageProvider = value;
                RaisePropertyChanged();
            }
        }
    }
}