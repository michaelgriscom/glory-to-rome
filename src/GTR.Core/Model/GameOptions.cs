#region

using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Game
{
    public class GameOptions : ObservableModel
    {
        public int MaxPlayers { get; set; }
        public string DeckName { get; set; }
    }
}