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

    public class OrderCardSerialization : CardSerialization
    {

    }

    public class BuildingFoundationSerialization : CardSerialization
    {

    }

    public class JackCardSerialization : CardSerialization
    {
    }
}