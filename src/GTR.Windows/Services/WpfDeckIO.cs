#region

using System;
using System.Collections.Generic;
using System.IO;
using GTR.Core.Services;
using GTR.Windows.Properties;

#endregion

namespace GTR.Windows.Services
{
    internal class WpfDeckIo : IDeckIo
    {
        private const string DeckFileExtension = ".deck";

        private static readonly string DeckDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private static readonly string deckFilePattern = "*" + DeckFileExtension;

        public IEnumerable<string> GetCustomDeckNames()
        {
            return Directory.EnumerateFiles(DeckDirectory, deckFilePattern);
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

        public string RepublicDeckSerialization
        {
            get { return Resources.republicdeck; }
        }

        public string ImperiumDeckSerialization
        {
            get { return Resources.imperialdeck; }
        }

        public void CreateDeck(string deckName, string deckSerialization)
        {
            string filepath = GetFilePath(deckName);
            File.WriteAllText(filepath, deckSerialization);
        }

        public bool RemoveDeck(string deckName)
        {
            bool fileExists = DeckFileExists(deckName);
            if (fileExists)
            {
                string filepath = GetFilePath(deckName);
                File.Delete(filepath);
            }
            return fileExists;
        }

        private string GetFilePath(string deckName)
        {
            return Path.Combine(DeckDirectory, string.Concat(deckName, DeckFileExtension));
        }

        private bool DeckFileExists(string deckName)
        {
            string filepath = GetFilePath(deckName);
            return File.Exists(filepath);
        }
    }
}