#region

using System.Collections.Generic;
using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class JackDeck : CardSourceTarget<JackCardModel>
    {
        public JackDeck(string name = "") : base(name)
        {
        }

        public JackDeck(IEnumerable<JackCardModel> cards) : base(cards)
        {
        }
    }
}