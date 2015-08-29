using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.Engine;
using GTR.Core.Game;

namespace GTR.Core.Services
{
    public interface IPlayerInputService
    {
        Task<Lead> RequestLeadAsync(LeadRequest leadRequest);
        Task<Follow> RequestFollowAsync(FollowRequest followRequest);
        Task<IAction> RequestMoveAsync(MoveRequest moveRequest);
    }

    public class MoveRequest
    {
        public Player Player;

        public MoveSpace MoveSpace;
    }

    public class LeadRequest : MoveRequest
    {
    }

    public class FollowRequest : MoveRequest
    {
        public RoleType RoleType;
    }
}
