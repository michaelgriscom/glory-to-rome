#region

using System.Threading.Tasks;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Moves;

#endregion

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
        public MoveSpace MoveSpace;
        public Player Player;
    }

    public class LeadRequest : MoveRequest
    {
    }

    public class FollowRequest : MoveRequest
    {
        public RoleType RoleType;
    }
}