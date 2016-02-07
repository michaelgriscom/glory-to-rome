using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GTR.Core.Moves;
using GTR.Core.Services;

namespace GTR.Server.Services
{
    public class PlayerInputService : IPlayerInputService
    {
        public async Task<Lead> RequestLeadAsync(LeadRequest leadRequest)
        {
            await Task.Delay(new TimeSpan(1, 0, 0));
            throw new NotImplementedException();

        }

        public async Task<Follow> RequestFollowAsync(FollowRequest followRequest)
        {
            await Task.Delay(new TimeSpan(1, 0, 0));

            throw new NotImplementedException();
        }

        public async Task<IAction> RequestMoveAsync(MoveRequest moveRequest)
        {
            await Task.Delay(new TimeSpan(1, 0, 0));

            throw new NotImplementedException();
        }
    }
}