#region

using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.DeckManagement
{
    public static class CardSetSerializer
    {
        /// <summary>
        ///     Loads the cards from a properly formated XML file (see CardsSchema.xsd) into a card set. Callers are responsible
        ///     for handling exceptions.
        /// </summary>
        public static CardSet Deserialize(string cardFileContents)
        {
            CardSet cardSet = new CardSet();
            XDocument cardXml = XDocument.Parse(cardFileContents);

            XName cardElementName = QualifyName("card");
            XName buildingElementName = QualifyName("building");
            XName materialElementName = QualifyName("material");
            XName descriptionElementName = QualifyName("description");

            var cardElements = from card in cardXml.Descendants(cardElementName)
                select new
                {
                    building = card.Element(buildingElementName).Value,
                    material = card.Element(materialElementName).Value,
                    description = card.Element(descriptionElementName).Value
                };

            foreach (var cardElement in cardElements)
            {
                string building = cardElement.building;
                string description = cardElement.description;
                RoleType roleType = ParseMaterialToRole(cardElement.material);
                OrderCardModel card = new OrderCardModel(building, description, roleType);
                cardSet.Add(card);
                Debug.WriteLine(card.Name);
            }
            return cardSet;
        }

        public static string Serialize(CardSet cardSet)
        {
            throw new NotImplementedException();
        }

        private static XName QualifyName(string element)
        {
            const string nameSpace = "http://tempuri.org/CardsSchema.xsd";
            return XName.Get(element, nameSpace);
        }

        private static RoleType ParseMaterialToRole(string material)
        {
            MaterialType materialType;
            bool parseSuccess = Enum.TryParse(material, true, out materialType);
            if (!parseSuccess)
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid material: {0}", material));
            }
            return materialType.ToRole();
        }
    }
}