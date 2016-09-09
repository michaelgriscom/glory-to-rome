#region

using System;
using System.Collections.Generic;
using System.Linq;
using GTR.Core.Game;

#endregion

namespace GTR.Core.Services
{
    public interface IDeckIo
    {
        string RepublicDeckSerialization { get; }
        string ImperiumDeckSerialization { get; }
        IEnumerable<string> GetCustomDeckNames();
        string GetCustomDeck(string deckName);
        bool IsCustomDeck(string deckName);
        void CreateDeck(string deckName, string deckSerialization);
        bool RemoveDeck(string deckName);
    }

    public static class DeckIoExtensions
    {
        public static IEnumerable<string> GetCustomDecks(this IDeckIo deckIo)
        {
            return deckIo.GetCustomDeckNames().Select(deckIo.GetCustomDeck);
        }

        public static IEnumerable<string> GetBuiltinDecks(this IDeckIo deckIo)
        {
            yield return deckIo.ImperiumDeckSerialization;
            yield return deckIo.RepublicDeckSerialization;
        }

        public static string GetBuiltinDeck(this IDeckIo deckIo, string deckName)
        {
            BuiltInDeck deck;
            Enum.TryParse(deckName, true, out deck);
            switch (deck)
            {
                case BuiltInDeck.Imperium:
                    return deckIo.ImperiumDeckSerialization;

                case BuiltInDeck.Republic:
                    return deckIo.RepublicDeckSerialization;

                default:
                    throw new ArgumentException("Unrecognized deck.");
            }
        }
    }
}