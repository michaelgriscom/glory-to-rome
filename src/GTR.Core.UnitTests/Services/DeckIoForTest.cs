#region

using System.Collections.Generic;
using GTR.Core.Services;
using GTR.Core.UnitTests.Properties;

#endregion

namespace GTR.Core.UnitTests.Services
{
    internal class DeckIoForTest : IDeckIo
    {
        private readonly Dictionary<string, string> _customDecks;

        public DeckIoForTest()
        {
            _customDecks = new Dictionary<string, string>();
        }

        public IEnumerable<string> GetCustomDeckNames()
        {
            return _customDecks.Keys;
        }

        public string RepublicDeckSerialization
        {
            get { return Resources.republicdeck; }
        }

        public string ImperiumDeckSerialization
        {
            get { return Resources.imperialdeck; }
        }

        public string GetCustomDeck(string deckName)
        {
            return _customDecks[deckName];
        }

        public bool IsCustomDeck(string deckName)
        {
            return _customDecks.ContainsKey(deckName);
        }

        public void CreateDeck(string deckName, string deckSerialization)
        {
            _customDecks.Add(deckName, deckSerialization);
        }

        public bool RemoveDeck(string deckName)
        {
            return _customDecks.Remove(deckName);
        }

        public IEnumerable<string> GetCustomDecks()
        {
            return _customDecks.Values;
        }
    }
}