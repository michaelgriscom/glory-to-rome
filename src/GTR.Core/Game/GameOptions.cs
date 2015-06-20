namespace GTR.Core.Game
{
    public class GameOptions
    {
        public GameOptions(string deckName)
        {
            DeckName = deckName;
        }

        public string DeckName { get; private set; }
    }
}