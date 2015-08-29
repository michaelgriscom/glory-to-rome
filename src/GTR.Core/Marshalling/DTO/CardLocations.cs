#region

using GTR.Core.Game;

#endregion

namespace GTR.Core.Serialization
{
    public class CardLocationKindSerialization
    {
        public int Id;
        public CardLocationKind Kind;
        public int PlayerId;
        public LocationScope Scope;
    }

    public class CardLocationSerialization
    {
        public CardSerialization[] Cards;
        public CardLocationKindSerialization LocationKind;
    }

    public enum LocationScope
    {
        Global,
        Player
    }

    public class SiteDeck : CardLocationSerialization
    {
        public MaterialType Material;
    }


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
        Pool
    }
}