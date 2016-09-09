using GTR.Core.Game;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GTR.Core.Model;

namespace GTR.Server.DataObjects
{
    public class GameEntity : EntityData
    {
        public PlayerEntity Host { get; set; }

        public List<PlayerEntity> Players { get; set; }
        //[NotMapped]
        //public string[] Players
        //{
        //    get
        //    {
        //        return this.InternalData.Split(',');
        //    }
        //    set
        //    {
        //        this.InternalData = string.Join(",", value);
        //    }
        //}

        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public string InternalData { get; set; }

        public GameOptions GameOptions { get; set; }
    }
}