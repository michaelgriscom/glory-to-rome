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
        public async Task<CreateGameResponseSerialization> PostCreate(CreateGameRequest request)
        {
            CreateGameResponseSerialization response = new CreateGameResponseSerialization();
            var playerId = GetPlayerId(request.AuthorizationToken);

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
        /// Joins a player into a game
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        private GameMarshaller gameMarshaller = new GameMarshaller();

        /// <summary>
        /// Starts a game
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
            string playerId = GetPlayerId(request.AuthorizationToken);
            if (playerId != gameInfo.HostId)
            {
                response.Message = ErrorMessages.NotGameHost;
                return response;
            }

           var game = gameManager.CreateGame(gameInfo);
            var gameDto = gameMarshaller.Marshall(game);

            await lobbyTable.DeleteLobbyGame(request.GameId, Request);

            response.Success = true;
            response.Game = gameDto;
            return response;
        }

        public async Task<GameInfoResponseSerialization> GetGameState(string gameId)
        {
            var game = gameManager.GetGameInfo(gameId);
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
            response.Game = gameMarshaller.Marshall(game);
            return response;
        }

        private GameManager gameManager = GameManager.Instance;

        private async Task StartGame(LobbyGame gameInfo)
        {
            gameManager.CreateGame(gameInfo);
            /*
            // Get the settings for the server project.
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Get the Notification Hubs credentials for the Mobile App.
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // Define a WNS payload
            var windowsToastPayload = @"<toast><visual><binding template=""ToastText01""><text id=""1"">"
                                    + "testing!" + @"</text></binding></visual></toast>";

            try
            {
                // Send the push notification and log the results.
                var result = await hub.SendWindowsNativeNotificationAsync(windowsToastPayload);

                // Write the success result to the logs.
                config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                // Write the failure result to the logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
            }
            */
        }

        private string GetPlayerId(int authorizationToken)
        {
            throw new NotImplementedException();
        }
    }
}
