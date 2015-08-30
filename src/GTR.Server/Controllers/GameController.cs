using System;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Core.Serialization;
using GTR.Core.Services;
using GTR.Server;
using Microsoft.Azure.Mobile.Server.Config;

namespace GTR.Server.Controllers
{
    [MobileAppController]
    public class GameController : ApiController
    {
        private GameManager gameManager = GameManager.Instance;

        public async Task<GameInfoResponseSerialization> GetGameState(string gameId)
        {
            var game = gameManager.GetGameDtoInfo(gameId);
            var response = new GameInfoResponseSerialization()
            {
                Success = false
            };

            if (game == null)
            {
                response.Message = ErrorMessages.NonexistentGame;
                return response;
            }

            response.Success = true;
            response.Game = game;
            return response;
        }

        public async Task<MoveResponseSerialization> PostMove(MoveSetRequest moveSetRequest)
        {
            MoveResponseSerialization response = new MoveResponseSerialization {Success = false};

            if (!IsValidToken(moveSetRequest.AuthorizationToken, moveSetRequest.GameId))
            {
                response.Message = ErrorMessages.InvalidAuth;
                return response;
            }

            string playerId = GetPlayerId(moveSetRequest.AuthorizationToken, moveSetRequest.GameId);

            bool moveSuccess = gameManager.MakeMove(playerId, moveSetRequest);

            if (!PerformMove(moveSetRequest))
            {
                response.Message = ErrorMessages.IllegalMove;
                return response;
            }
            response.Success = true;
            return response;
        }

        private bool PerformMove(MoveSetRequest moveSetRequest)
        {
            return gameManager.MakeMove(moveSetRequest.GameId, moveSetRequest);
        }


        private bool IsValidToken(int authorizationToken, string gameId)
        {
            throw new NotImplementedException();
        }

        private string GetPlayerId(int authorizationToken, string gameId)
        {
            throw new NotImplementedException();
        }
    }
}
