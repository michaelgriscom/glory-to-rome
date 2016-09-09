#region

using System;

#endregion

namespace GTR.Core.DeckManagement
{
    public static class DeckTypeSerializer
    {
        private const char FieldSeparator = ',';
        private static readonly string RowSeparator = Environment.NewLine;

        /// <summary>
        ///     Loads the cards from a properly formated CSV file deck version. Callers are responsible for handling exceptions.
        ///     File format:
        ///     deckName
        ///     Dock,4
        ///     Palisade,3
        ///     Bridge,2
        /// </summary>
        public static DeckType Deserialize(string deckFileContents)
        {
            string[] rows = deckFileContents.Split(new[] {RowSeparator}, StringSplitOptions.RemoveEmptyEntries);

            // parse header
            string name = rows[0];
            DeckType deckVersion = new DeckType(name);

            // loop through rest of file
            for (int i = 1; i < rows.Length; i++)
            {
                string row = rows[i];
                string[] rowVals = row.Split(FieldSeparator);
                string cardName = rowVals[0];
                int cardCount = int.Parse(rowVals[1]);
                deckVersion.AddCard(cardName, cardCount);
            }
            return deckVersion;
        }

        public static string Serialize(DeckType deckVersion)
        {
            string header = GetHeader(deckVersion);
            var cardNames = deckVersion.GetCardNames();
            string[] rows = new string[cardNames.Count + 1]; // need extra entry for header

            rows[0] = header;
            int rowNum = 1;
            foreach (var cardName in cardNames)
            {
                int cardCount = deckVersion.GetCount(cardName);
                string row = string.Format("{0}{1}{2}", cardName, FieldSeparator, cardCount);
                rows[rowNum] = row;
                rowNum++;
            }
            string serialization = string.Join(RowSeparator, rows);
            return serialization;
        }

        private static string GetHeader(DeckType deckVersion)
        {
            return deckVersion.DeckName;
        }
    }
}