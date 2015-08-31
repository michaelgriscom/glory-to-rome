﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.DeckManagement;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Services;
using GTR.Core.Util;

namespace GTR.Core.Marshalling
{
    class JackCardMarshaller : IMarshaller<JackCardModel, CardSerialization>
    {
        public CardSerialization Marshall(JackCardModel poco)
        {
            var dto = new CardSerialization()
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

            var poco = new JackCardModel()
            {
                Id = dto.Id
            };
            return poco;
        }
    }

    class BuildSiteMarshaller : IMarshaller<BuildingSite, CardSerialization>
    {
        public CardSerialization Marshall(BuildingSite poco)
        {
            var dto = new BuildingFoundationSerialization()
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

            var poco = new BuildingSite((MaterialType)dto.Material, (SiteType)dto.SiteType);
            poco.Id = dto.Id;
            return poco;
        }
    }

    class OrderCardMarshaller : IMarshaller<OrderCardModel, CardSerialization>
    {
        private CardSet cardSet;

        public OrderCardMarshaller(CardSet cardSet)
        {
            this.cardSet = cardSet;
        }

        public CardSerialization Marshall(OrderCardModel poco)
        {
            var dto = new CardSerialization()
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
            var poco = cardSet.MakeCard(dto.BuildingName);
      
            poco.Id = dto.Id;
            return poco;
        }
    }
}
