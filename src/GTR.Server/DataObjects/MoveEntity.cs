using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTR.Core.Game;
using GTR.Core.Marshalling.DTO;
using Microsoft.Azure.Mobile.Server;

namespace GTR.Server.DataObjects
{
    public class MoveEntity : EntityData
    {
        public string PlayerId { get; set; }

        public int PlayerSeat { get; set; }

        public string GameId { get; set; }

        public string CardId { get; set; }

        public string DestinationId { get; set; }

        public string SourceId { get; set; }

        public int MoveSetNumber { get; set; }
    }

    public class CardLocation : EntityData
    {
        public string GameId { get; set; }

        public LocationScope Scope { get; set; }

        public CardLocationKind Kind { get; set; }
    }

    public class CardEntity : EntityData
    {
        public string GameId { get; set; }

        public MaterialType Material { get; set; }

        public string CardName { get; set; }

        public CardType CardType { get; set; }

        public SiteType SiteType { get; set; }
    }

    public enum CardType
    {
        Order, Jack, Foundation
    }
}