#region

using System.Threading.Tasks;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Services
{
    internal interface IMatchmakingService
    {
        Task<CreateGameResponseSerialization> CreateGameAsync();
        Task<JoinGameResponseSerialization> JoinGameAsync(JoinGameRequest request);
        Task<ListGamesResponseSerialization> ListGamesAsync();
    }
}