using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Core.Serialization;
using GTR.Core.Services;
using GTR.Server;
using GTR.Server.Controllers;
using GTR.Server.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;

namespace GTR.Server.Controllers
{
    [MobileAppController]
    public class MatchmakingController : ApiController
    {
        public MatchmakingController()
        {
            lobbyTable = new LobbyTable();
        }
        private LobbyTable lobbyTable;

        /// <summary>
        /// Creates a game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateGameResponseSerialization> PostHost(CreateGameRequest request)
        {
            CreateGameResponseSerialization response = new CreateGameResponseSerialization();
            var playerId = GetPlayerId();

            LobbyGame game = new LobbyGame()
            {
                Players = new List<string>() { playerId },
                GameOptions = request.GameOptions,
                HostId = playerId
            };

            response.Success = true;
            game = await lobbyTable.AddLobbyGame(game, Request);

            response.GameId = game.Id;

            return response;
        }

        /// <summary>
        /// Starts a game
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<StartGameResponseSerialization> GetCreate(string GameId)
        {
            var response = new StartGameResponseSerialization();
            response.Success = false;

            // TODO: query
            var allGames = lobbyTable.GetAllLobbyGames(Request);
            var gameInfo = allGames.First(g => g.Id == GameId);

            if (gameInfo == null)
            {
                response.Message = ErrorMessages.NonexistentGame;
                return response;
            }
            string playerId = GetPlayerId();
            if (playerId != gameInfo.HostId)
            {
                response.Message = ErrorMessages.NotGameHost;
                return response;
            }

           var game = gameManager.CreateGame(gameInfo);

            await lobbyTable.DeleteLobbyGame(GameId, Request);

            response.Success = true;
            response.Game = game;
            return response;
        }

        private GameManager gameManager = GameManager.Instance;

        private string GetPlayerId()
        {
            return "1";
        }
    }
}
