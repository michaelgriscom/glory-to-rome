#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.Game
{
    public class GameOptions : ObservableModel
    {
        public int MaxPlayers { get; set; }
        public string DeckName { get; set; }
    }
}