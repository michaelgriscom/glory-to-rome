using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace GTR.Server.DataObjects
{
    public class PlayerEntity : EntityData
    {
        [Required]
        public string Name { get; set; }

        public List<GameEntity> Games { get; set; } 
    }
}