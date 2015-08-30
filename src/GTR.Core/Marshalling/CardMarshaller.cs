using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Model;
using GTR.Core.Serialization;

namespace GTR.Core.Marshalling
{
    class CardMarshaller : IMarshaller<CardModelBase, CardSerialization>
    {
        private Dictionary<int, CardModelBase> cardDictionary;

        public CardMarshaller(Dictionary<int, CardModelBase> cardDictionary)
        {
            this.cardDictionary = cardDictionary;
        }

        public CardSerialization Marshall(CardModelBase poco)
        {
            var dto = new CardSerialization();
            dto.Id = poco.Id;
            return dto;
        }

        public CardModelBase UnMarshall(CardSerialization slimRepresentation)
        {
            if (!cardDictionary.ContainsKey(slimRepresentation.Id))
            {
                throw new ArgumentException("Invalid card id.");
            }
            return cardDictionary[slimRepresentation.Id];
        }
    }
}
