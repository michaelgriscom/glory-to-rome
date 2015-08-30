using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GTR.Core.Action;
using GTR.Core.Services;

namespace GTR.Server.Services
{
    public class PlayerInputService : IPlayerInputService
    {
        public Task<Lead> RequestLeadAsync(LeadRequest leadRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Follow> RequestFollowAsync(FollowRequest followRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IAction> RequestMoveAsync(MoveRequest moveRequest)
        {
            throw new NotImplementedException();
        }
    }
}