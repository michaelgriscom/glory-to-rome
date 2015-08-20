#region

using System.Threading.Tasks;
using GTR.Core.Game;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Services
{
    public interface IMatchmakingService
    {
        Task<CreateGameResponseSerialization> CreateGameAsync();
        Task<JoinGameResponseSerialization> JoinGameAsync(JoinGameRequest request);
        Task<ListGamesResponseSerialization> ListGamesAsync();
        Task<StartGameResponseSerialization> StartGameAsync(StartGameRequest request);
    }

    public class StartGameRequest : Request
    {
        public int GameId;
    }

    public class StartGameResponseSerialization : ResponseSerialization
    {

    }

    public class CreateGameRequest : Request
    {
        public GameOptions GameOptions;
    }

}