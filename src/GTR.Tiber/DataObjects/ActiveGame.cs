using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTR.Tiber.DataObjects
{
    public class ActiveGame : EntityData
    {
        public bool IsActive { get; set; }

        public int Id { get; set; }
    }
}