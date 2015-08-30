using GTR.Core.Game;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTR.Server.DataObjects
{
    public class LobbyGame : EntityData
    {
        public string HostId { get; set; }

        public List<string> Players { get; set; }

        public GameOptions GameOptions { get; set; }
    }
}