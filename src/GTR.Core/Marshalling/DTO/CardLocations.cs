#region

using GTR.Core.Game;

#endregion

namespace GTR.Core.Marshalling.DTO
{
    public class CardLocationKindSerialization
    {
        public CardLocationKind Kind;
        public string PlayerId;
        public LocationScope Scope;
        public CardType CardType;
    }

    public class CardLocationDto : IDto
    {
        public CardSerialization[] Cards;
        public string Id;
        public CardLocationKindSerialization LocationKind;
    }

    public enum LocationScope
    {
        Global,
        Player,
        Private
    }

    //public class SiteDeck : CardLocationDto
    //{
    //    public MaterialType Material;
    //}


    public enum CardLocationKind
    {
        Clientele,
        Hand,
        Vault,
        PlayArea,
        Stockpile,
        DemandArea,
        BuildArea,
        CompletedBuildings,
        CompletedFoundations,
        JackDeck,
        OrderDeck,
        Pool,
        SiteDeck
    }
}