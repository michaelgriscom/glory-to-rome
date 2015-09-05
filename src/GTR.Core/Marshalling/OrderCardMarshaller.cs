#region

using System;
using GTR.Core.DeckManagement;
using GTR.Core.Game;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Marshalling
{
    internal class JackCardMarshaller : IMarshaller<JackCardModel, CardSerialization>
    {
        public CardSerialization Marshall(JackCardModel poco)
        {
            var dto = new JackCardSerialization
            {
                CardType = CardType.Jack,
                Id = poco.Id
            };
            return dto;
        }

        public JackCardModel UnMarshall(CardSerialization dto)
        {
            if (dto.CardType != CardType.Jack)
            {
                throw new ArgumentException("Invalid card type.");
            }

            var poco = new JackCardModel
            {
                Id = dto.Id
            };
            return poco;
        }
    }

    internal class BuildSiteMarshaller : IMarshaller<BuildingSite, CardSerialization>
    {
        public CardSerialization Marshall(BuildingSite poco)
        {
            var dto = new BuildingFoundationSerialization
            {
                CardType = CardType.BuildingSite,
                Id = poco.Id,
                Material = poco.MaterialType,
                SiteType = poco.SiteType
            };
            return dto;
        }

        public BuildingSite UnMarshall(CardSerialization dto)
        {
            if (dto.CardType != CardType.BuildingSite)
            {
                throw new ArgumentException("Invalid card type.");
            }

            var poco = new BuildingSite((MaterialType) dto.Material, (SiteType) dto.SiteType);
            poco.Id = dto.Id;
            return poco;
        }
    }

    internal class OrderCardMarshaller : IMarshaller<OrderCardModel, CardSerialization>
    {
        private readonly IOrderCardMaker cardMaker;

        public OrderCardMarshaller(IOrderCardMaker cardMaker)
        {
            this.cardMaker = cardMaker;
        }

        public CardSerialization Marshall(OrderCardModel poco)
        {
            var dto = new OrderCardSerialization
            {
                BuildingName = poco.Name,
                CardType = CardType.Order,
                Id = poco.Id,
                Material = poco.GetMaterialType()
            };
            return dto;
        }

        public OrderCardModel UnMarshall(CardSerialization dto)
        {
            if (dto.CardType != CardType.Order)
            {
                throw new ArgumentException("Invalid card type.");
            }
            var poco = cardMaker.MakeCard(dto.BuildingName);

            poco.Id = dto.Id;
            return poco;
        }
    }
}