#region

using System;
using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Services
{
    public interface IPlayerInput
    {
        void RequestLead(PlayerRequest request);
        void RequestFollow(PlayerRequest request, RoleType role);
        event LeadHandler OnLead;
        event ThinkHandler OnThink;
        event FollowHandler OnFollow;
        event MoveHandler OnMove;
    }

    public delegate void LeadHandler(object sender, LeadEventArgs e);

    public delegate void ThinkHandler(object sender, ResponseEventArgs e);

    public delegate void FollowHandler(object sender, FollowEventArgs e);

    public delegate void MoveHandler(object sender, MoveEventArgs e);

    public class Lead
    {
        public ICollection<HandCardModel> Cards;
        public RoleType Role;
        public bool IsThink;
        public MoveSpace MoveSpace { get; private set; }

    }

    public class LeadEventArgs : ResponseEventArgs
    {
        public Lead Lead;
    }

    public class FollowEventArgs : ResponseEventArgs
    {
        public Follow Follow;
    }

    public class Follow
    {
        public ICollection<HandCardModel> Cards;
        public bool IsThink;
    }

    public class MoveEventArgs : ResponseEventArgs
    {
        public IAction Move;
    }

    public class PlayerRequest
    {
        public int Id { get; private set; }
        public DateTime CreationTime { get; private set; }
        public MoveSpace MoveSpace { get; private set; }
    }

    public class ResponseEventArgs : EventArgs
    {
        private int RequestId { get; set; }
    }
}