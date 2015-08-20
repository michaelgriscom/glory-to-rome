namespace GTR.Core.Game
{
    public class GameOptions
    {
        public GameOptions(string deckName, int maxPlayers)
        {
            MaxPlayers = maxPlayers;
            DeckName = deckName;
        }

        public int MaxPlayers { get; private set; }

        public string DeckName { get; private set; }
    }
}