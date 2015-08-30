using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.DeckManagement;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Services;

namespace GTR.Core.Marshalling
{
    class CardMarshaller : IMarshaller<CardModelBase, CardSerialization>
    {
        private CardSet cardSet;

        public CardMarshaller(CardSet cardSet)
        {
            this.cardSet = cardSet;
        }

        public CardSerialization Marshall(CardModelBase poco)
        {
            CardSerialization dto = poco.ToDto();
            return dto;
        }

        public CardModelBase UnMarshall(CardSerialization dto)
        {
            CardModelBase poco;
            switch (dto.CardType)
            {
                case CardType.Order:
                    poco = cardSet.MakeCard(dto.BuildingName);
                    break;
                case CardType.Jack:
                    poco = new JackCardModel();
                    break;
                case CardType.BuildingSite:
                    poco = new BuildingSite((MaterialType)dto.Material, (SiteType)dto.SiteType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            poco.Id = dto.Id;
            return poco;
        }
    }
}
