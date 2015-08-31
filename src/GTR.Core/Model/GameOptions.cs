#region

#endregion

namespace GTR.Core.Model
{
    public class GameOptions : ObservableModel
    {
        private string _deckName;
        private int _maxPlayers;

        public int MaxPlayers
        {
            get { return _maxPlayers; }
            set
            {
                _maxPlayers = value;
                RaisePropertyChanged();
            }
        }

        public string DeckName
        {
            get { return _deckName; }
            set
            {
                _deckName = value;
                RaisePropertyChanged();
            }
        }
    }
}