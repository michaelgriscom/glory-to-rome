using GTR.Core.Engine;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Util;

namespace GTR.Core.Game
{
    public class Game : ObservableModel
    {
        private GameOptions _gameOptions;

        public GameOptions GameOptions
        {
            get { return _gameOptions; }
            set { _gameOptions = value; RaisePropertyChanged(); }
        }

        private Player _actionPlayer;

        public Player ActionPlayer
        {
            get { return _actionPlayer; }
            set { _actionPlayer = value; }
        }


        private Player _leadPlayer;

        public Player LeadPlayer
        {
            get { return _leadPlayer; }
            set { _leadPlayer = value; RaisePropertyChanged();}
        }

        private GameTable _gameTable;

        public GameTable GameTable
        {
            get { return _gameTable; }
            set { _gameTable = value; RaisePropertyChanged(); }
        }

        private int _turnNumber;

        public int TurnNumber
        {
            get { return _turnNumber; }
            set { _turnNumber = value; RaisePropertyChanged(); }
        }

    }
}