#region

using System.Collections.ObjectModel;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Model
{
    public class Game : ObservableModel
    {
        private GameOptions _gameOptions;
        private GameTable _gameTable;
        private Player _leadPlayer;
        private int _turnNumber;

        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CardModelBase> _cards;

        public ObservableCollection<CardModelBase> Cards
        {
            get { return _cards; }
            set { _cards = value; }
        }


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