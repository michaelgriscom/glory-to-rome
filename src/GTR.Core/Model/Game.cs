#region

using GTR.Core.Engine;
using GTR.Core.Model;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Game
{
    public class Game : ObservableModel
    {
        private GameOptions _gameOptions;
        private GameTable _gameTable;
        private Player _leadPlayer;
        private int _turnNumber;

        public GameOptions GameOptions
        {
            get { return _gameOptions; }
            set
            {
                _gameOptions = value;
                RaisePropertyChanged();
            }
        }

        public Player ActionPlayer { get; set; }

        public Player LeadPlayer
        {
            get { return _leadPlayer; }
            set
            {
                _leadPlayer = value;
                RaisePropertyChanged();
            }
        }

        public GameTable GameTable
        {
            get { return _gameTable; }
            set
            {
                _gameTable = value;
                RaisePropertyChanged();
            }
        }

        public int TurnNumber
        {
            get { return _turnNumber; }
            set
            {
                _turnNumber = value;
                RaisePropertyChanged();
            }
        }
    }
}