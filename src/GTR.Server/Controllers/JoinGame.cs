using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GTR.Core.Serialization;

namespace GTR.Server.Controllers
{
    public class JoinGame
    {
        /// <summary>
        /// Joins a player into a game
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<JoinGameResponseSerialization> GetJoin(JoinGameRequest request)
        {
            var response = new JoinGameResponseSerialization();
            response.Success = false;

            //// TODO: query
            //var allGames = lobbyTable.GetAllLobbyGames(Request);
            //var gameInfo = allGames.First(g => g.Id == request.GameId);

            //if (gameInfo == null)
            //{
            //    response.Message = ErrorMessages.NonexistentGame;
            //    return response;
            //}
            //var playerId = GetPlayerId(request.AuthorizationToken);
            //if (gameInfo.Players.Any(pId => pId == playerId))
            //{
            //    response.Message = ErrorMessages.AlreadyJoined;
            //    return response;
            //}
            //if (gameInfo.Players.Count >= gameInfo.GameOptions.MaxPlayers)
            //{
            //    response.Message = ErrorMessages.GameFull;
            //    return response;
            //}

            //gameInfo.Players.Add(playerId);
            //response.Success = true;
            return response;
        }
    }
}