using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Tiber.Services;
using GTR.Core.Game;
using GTR.Core.Serialization;
using GTR.Core.Services;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using GTR.Tiber.DataObjects;
using tiberService.Models;

namespace GTR.Tiber.Controllers
{
    [MobileAppController]
    public class MatchmakingController : ApiController
    {
        public MatchmakingController()
        {
            lobbyTable = new LobbyTable();
        }
        private LobbyTable lobbyTable;

        public async Task<CreateGameResponseSerialization> PostCreate(CreateGameRequest request)
        {
            CreateGameResponseSerialization response = new CreateGameResponseSerialization();
            var playerId = GetPlayerId(request.AuthorizationToken);

            LobbyGame game = new LobbyGame()
            {
                Players = new List<int>() { playerId },
                GameOptions = request.GameOptions,
                HostId = playerId
            };

            response.Success = true;
            game = await lobbyTable.AddLobbyGame(game, Request);

            response.GameId = game.Id;

            return response;
        }

        public async Task<JoinGameResponseSerialization> PostJoin(JoinGameRequest request)
        {
            var response = new JoinGameResponseSerialization();
            response.Success = false;

            // TODO: query
            var allGames = lobbyTable.GetAllLobbyGames(Request);
            var gameInfo = allGames.First(g => g.Id == request.GameId);

            if (gameInfo == null)
            {
                response.Message = ErrorMessages.NonexistentGame;
                return response;
            }
            var playerId = GetPlayerId(request.AuthorizationToken);
            if (gameInfo.Players.Any(pId => pId == playerId))
            {
                response.Message = ErrorMessages.AlreadyJoined;
                return response;
            }
            if (gameInfo.Players.Count >= gameInfo.GameOptions.MaxPlayers)
            {
                response.Message = ErrorMessages.GameFull;
                return response;
            }

            gameInfo.Players.Add(playerId);
            response.Success = true;
            return response;
        }

        public async Task<StartGameResponseSerialization> PostStart(StartGameRequest request)
        {
            var response = new StartGameResponseSerialization();
            response.Success = false;

            // TODO: query
            var allGames = lobbyTable.GetAllLobbyGames(Request);
            var gameInfo = allGames.First(g => g.Id == request.GameId);

            if (gameInfo == null)
            {
                response.Message = ErrorMessages.NonexistentGame;
                return response;
            }
            int playerId = GetPlayerId(request.AuthorizationToken);
            if (playerId != gameInfo.HostId)
            {
                response.Message = ErrorMessages.NotGameHost;
                return response;
            }

            StartGame(gameInfo);
            await lobbyTable.DeleteLobbyGame(request.GameId, Request);

            response.Success = true;
            return response;
        }

        private GameManager gameManager = GameManager.Instance;

        private void StartGame(LobbyGame gameInfo)
        {
            gameManager.StartGame(gameInfo);
        }

        private int GetPlayerId(int authorizationToken)
        {
            throw new NotImplementedException();
        }
    }
}
