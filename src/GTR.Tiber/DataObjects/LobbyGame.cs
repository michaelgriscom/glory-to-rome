using GTR.Core.Game;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTR.Tiber.DataObjects
{
    public class LobbyGame : EntityData
    {
        public bool IsStarted { get; set; }

        public int HostId { get; set; }

        public List<int> Players { get; set; }

        public int MaxPlayers { get; set; }

        public GameOptions GameOptions { get; set; }
    }
}