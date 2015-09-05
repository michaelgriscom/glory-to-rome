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
    internal class JackCardMarshaller : IMarshaller<JackCardModel, JackCardSerialization>
    {
        public JackCardSerialization Marshall(JackCardModel poco)
        {
            var dto = new JackCardSerialization
            {
                CardType = CardType.Jack,
                Id = poco.Id
            };
            return dto;
        }

        public JackCardModel UnMarshall(JackCardSerialization dto)
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

    internal class BuildSiteMarshaller : IMarshaller<BuildingSite, BuildingFoundationSerialization>
    {
        public BuildingFoundationSerialization Marshall(BuildingSite poco)
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

        public BuildingSite UnMarshall(BuildingFoundationSerialization dto)
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

    internal class OrderCardMarshaller : IMarshaller<OrderCardModel, OrderCardSerialization>
    {
        private readonly IOrderCardMaker cardMaker;

        public OrderCardMarshaller(IOrderCardMaker cardMaker)
        {
            this.cardMaker = cardMaker;
        }

        public OrderCardSerialization Marshall(OrderCardModel poco)
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

        public OrderCardModel UnMarshall(OrderCardSerialization dto)
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