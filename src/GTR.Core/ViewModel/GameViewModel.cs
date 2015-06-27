#region

using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.ViewModel
{
    public class GameViewModel : ObservableObject
    {
        private PlayerBoard _playerBoard;

        public GameViewModel(PlayerBoard player)
        {
            Player = player;
        }

        public PlayerBoard Player
        {
            get { return _playerBoard; }
            set
            {
                _playerBoard = value;
                RaisePropertyChanged();
            }
        }
    }
}