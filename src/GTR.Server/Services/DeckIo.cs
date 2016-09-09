#region

using System.Collections.Generic;
using System.IO;
using GTR.Core.Services;

#endregion

namespace GTR.Server.Services
{
    internal class DeckIo : IDeckIo
    {
        public IEnumerable<string> GetCustomDeckNames()
        {
            return new string[0];
        }

        public string GetCustomDeck(string deckName)
        {
            string filepath = GetFilePath(deckName);
            return File.ReadAllText(filepath);
        }

        public bool IsCustomDeck(string deckName)
        {
            return DeckFileExists(deckName);
        }

        public string RepublicDeckSerialization { get; } = @"Republic
Academy,4
Atrium,4
Bath,4
Foundry,4
Gate,4
School,4
Shrine,4
Amphitheatre,4
Aqueduct,4
Bridge,4
Senate,4
Storeroom,4
Tower,4
Vomitorium,4
Wall,4
Basilica,4
Fountain,4
Forum,4
Ludus Magnus,4
Palace,4
Stairway,4
Statue,4
Temple,4
Bar,4
Insula,4
Latrine,4
Road,4
Catacomb,4
Circus Maximus,4
Coliseum,4
Garden,4
Prison,4
Scriptorium,4
Sewer,4
Villa,4
Circus,4
Dock,4
Market,4
Palisade,4";
        public string ImperiumDeckSerialization { get; } = @"Imperial
Academy,4
Atrium,4
Bath,4
Foundry,4
Gate,4
School,4
Shrine,4
Amphitheatre,4
Aqueduct,4
Bridge,4
Senate,4
Storeroom,4
Tower,4
Vomitorium,4
Wall,4
Basilica,4
Fountain,4
Forum,4
Ludus Magnus,4
Palace,4
Stairway,4
Statue,4
Temple,4
Bar,4
Insula,4
Latrine,4
Road,4
Catacomb,4
Circus Maximus,4
Coliseum,4
Garden,4
Prison,4
Scriptorium,4
Sewer,4
Villa,4
Circus,4
Dock,4
Market,4
Palisade,4";

        public void CreateDeck(string deckName, string deckSerialization)
        {
        }

        public bool RemoveDeck(string deckName)
        {
            return false;
        }

        private string GetFilePath(string deckName)
        {
            return string.Empty;
        }

        private bool DeckFileExists(string deckName)
        {
            return false;
        }
    }
}