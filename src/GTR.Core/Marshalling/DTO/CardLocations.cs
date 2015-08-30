#region

using GTR.Core.Game;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Marshalling.DTO
{
    public class CardLocationKindSerialization
    {
        public CardLocationKind Kind;
        public int PlayerId;
        public LocationScope Scope;
    }

    public class CardLocationDto : IDto
    {
        public CardSerialization[] Cards;
        public CardLocationKindSerialization LocationKind;
        public string Id;
    }

    public enum LocationScope
    {
        Global,
        Player
    }

    public class SiteDeck : CardLocationDto
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