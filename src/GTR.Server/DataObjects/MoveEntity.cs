using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GTR.Core.Game;
using GTR.Core.Marshalling.DTO;
using Microsoft.Azure.Mobile.Server;

namespace GTR.Server.DataObjects
{
    public class MoveEntity : EntityData
    {
        //public virtual Player Player { get; set; }

        public virtual GameEntity GameEntity { get; set; }

        public int CardId { get; set; }

        public string DestinationId { get; set; }

        public string SourceId { get; set; }

        public int MoveSetNumber { get; set; }

        public RoleType EffectiveRole { get; set; }

        public PlayerEntity OriginatingPlayer { get; set; }
    }
/*
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
    */
    public enum CardType
    {
        Order, Jack, Foundation
    }
}