using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Core.Serialization;
using GTR.Tiber.DataObjects;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;

namespace GTR.Tiber.Controllers
{
    [MobileAppController]
    public class GameController : ApiController
    {
        private GameManager gameManager = GameManager.Instance;

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
            return gameManager.MakeMove(moveSetRequest.GameId, moveSetRequest.MoveSet);
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
