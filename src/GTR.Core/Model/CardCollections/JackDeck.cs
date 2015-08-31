#region

using System.Collections.Generic;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public class JackDeck : ObservableCardCollection<JackCardModel>
    {
        public JackDeck()
        {
        }

        public JackDeck(IEnumerable<JackCardModel> cards) : base(cards)
        {
        }
    }
}