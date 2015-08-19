#region

using System.Threading.Tasks;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Services
{
    public interface IGameService
    {
        Task<MoveResponseSerialization> ExecuteMoveAsync();
        Task<GameStateResponseSerialization> GetGameStateAsync(GameStateRequest request);
        Task<MoveHistoryResponseSerialization> GetMoveHistoryAsync(MoveHistoryRequest request);
        event MoveRequestedHandler OnMoveRequested;
        event OpponentMoveReceivedHandler OnOpponentMove;
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