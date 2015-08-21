using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Core.Serialization;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;

namespace GTR.Tiber.Controllers
{
    [MobileAppController]
    public class GameController : ApiController
    {
        public async Task<MoveResponseSerialization> PostMove(MoveSetRequest request)
        {
            MoveResponseSerialization response = new MoveResponseSerialization {Success = false};

            if (!IsValidToken(request.AuthorizationToken, request.GameId))
            {
                response.Message = ErrorMessages.InvalidAuth;
                return response;
            }

            int playerId = GetPlayerId(request.AuthorizationToken, request.GameId);
            if (!WaitingForMove(playerId))
            {
                response.Message = ErrorMessages.NotPlayerTurn;
                return response;
            }

            if (!ValidateMove(request.MoveSet))
            {
                response.Message = ErrorMessages.IllegalMove;
                return response;
            }

            PerformMove(request.MoveSet);
            await RecordMove(request.MoveSet);
            response.Success = true;
            return response;
        }

        private async Task RecordMove(MoveSetSerialization moveSet)
        {
            throw new NotImplementedException();
        }

        private void PerformMove(MoveSetSerialization moveSet)
        {
            throw new NotImplementedException();
        }

        private bool ValidateMove(MoveSetSerialization moveSet)
        {
            throw new NotImplementedException();
        }

        private bool WaitingForMove(int playerId)
        {
            throw new NotImplementedException();
        }

        private bool IsValidToken(int authorizationToken, int gameId)
        {
            throw new NotImplementedException();
        }

        private int GetPlayerId(int authorizationToken, int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
