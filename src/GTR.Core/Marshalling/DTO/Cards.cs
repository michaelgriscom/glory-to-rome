#region

using GTR.Core.Game;
using Newtonsoft.Json;

#endregion

namespace GTR.Core.Serialization
{
    public class CardSerialization : IDto
    {
        public int Id;

        public MaterialType? Material;

        public string BuildingName;

        public SiteType? SiteType;

        public CardType CardType;
    }


    public enum CardType
    {
        Order,
        Jack,
        BuildingSite
    }

    public class MaterialCardSerialization : CardSerialization
    {
    }

    public class OrderCardSerialization : MaterialCardSerialization
    {
    }

    public class BuildingFoundationSerialization : MaterialCardSerialization
    {
    }

    public class JackCardSerialization : CardSerialization
    {
    }
}