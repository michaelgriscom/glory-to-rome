#region

using System.Collections.Generic;
using GTR.Core.Services;

#endregion

namespace GTR.Core.Design
{
    internal class DesignResourceProvider : IResourceProvider
    {
        public string RepublicDeckCsv
        {
            get { return ""; }
        }

        public string ImperialDeckCsv
        {
            get { return ""; }
        }

        public IEnumerable<string> CustomDecks
        {
            get { return new string[0]; }
        }

        public string CardXml
        {
            get { return ""; }
        }
    }
}