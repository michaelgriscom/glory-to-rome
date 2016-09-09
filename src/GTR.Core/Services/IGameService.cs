#region

using System.Threading.Tasks;
using GTR.Core.Marshalling.DTO;

#endregion

namespace GTR.Core.Services
{
    public interface IGameService
    {
        Task<MoveResponseSerialization> ExecuteMoveAsync(MoveSetSerialization move);
        Task<GameStateResponseSerialization> GetGameStateAsync(GameStateRequest request);
        Task<MoveHistoryResponseSerialization> GetMoveHistoryAsync(MoveHistoryRequest request);
        Task<EndGameResponseSerialization> EndGameAsync(EndGameRequest request);
        Task<LeaveGameResponseSerialization> LeaveGameAsync(LeaveGameRequest request);
        event MoveRequestedHandler OnMoveRequested;
        event OpponentMoveReceivedHandler OnOpponentMove;
        Task<CreateGameResponseSerialization> CreateGameAsync();
        Task<JoinGameResponseSerialization> JoinGameAsync(JoinGameRequest request);
        Task<ListGamesResponseSerialization> ListGamesAsync();
        Task<StartGameResponseSerialization> StartGameAsync(StartGameRequest request);
    }

    public delegate void MoveRequestedHandler(object sender, MoveRequestedHandlerArgs args);

    public class MoveRequestedHandlerArgs
    {
        public MoveSpaceSerialization MoveSpace;
    }

    public delegate void OpponentMoveReceivedHandler(object sender, OpponentMoveReceivedHandlerArgs args);

    public class OpponentMoveReceivedHandlerArgs
    {
        public ExecutedMoveSerialization Move;
    }
}