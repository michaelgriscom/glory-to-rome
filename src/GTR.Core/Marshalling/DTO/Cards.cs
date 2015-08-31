#region

using GTR.Core.Game;

#endregion

namespace GTR.Core.Marshalling.DTO
{
    public class CardSerialization : IDto
    {
        public string BuildingName;
        public CardType CardType;
        public int Id;
        public MaterialType? Material;
        public SiteType? SiteType;
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