using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Core.Services;
using GTR.Server;
using GTR.Server.Controllers;
using GTR.Server.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http.OData;
using GTR.Core.Marshalling.DTO;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;
using tiberService.Models;

namespace GTR.Server.Controllers
{
    [MobileAppController]
    public class MatchmakingController : ApiController
    {
        public MatchmakingController()
        {
        }

        /// <summary>
        /// Creates a game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateGameResponseSerialization> PostHost(CreateGameRequest request)
        {
            CreateGameResponseSerialization response = new CreateGameResponseSerialization();

            using (var context = new GtrDbContext())
            {
                PlayerEntity player = await context.Players.FirstOrDefaultAsync(p => p.Id == "1");
                if (player == null)
                {
                    var playerDomainManager = new EntityDomainManager<PlayerEntity>(context, Request);
                    player = await playerDomainManager.InsertAsync(new PlayerEntity()
                    {
                        Name = "TestUser",
                        Id = "1"
                    });
                }

                GameEntity game = new GameEntity()
                {
                    Players = new List<PlayerEntity>() { player },
                    GameOptions = request.GameOptions,
                    Host = player
                };
                response.Success = true;

                var gameDomainManager = new EntityDomainManager<GameEntity>(context, Request);
                game = await gameDomainManager.InsertAsync(game);

                await context.SaveChangesAsync();

                response.GameId = game.Id;

                return response;
            }
            //player = await GetPlayerId();
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

            using (var context = new GtrDbContext())
            {
                // TODO: query
                //var DomainManager = new EntityDomainManager<GameEntity>(context, Request);

                //var allGames = DomainManager.Query();
                var gameInfo = await context.Games.FirstOrDefaultAsync(g => g.Id == GameId);

                if (gameInfo == null)
                {
                    response.Message = ErrorMessages.NonexistentGame;
                    return response;
                }

                var player = await context.Players.FirstOrDefaultAsync(p => p.Id == "1");
                if (player.Id != gameInfo.Host.Id)
                {
                    response.Message = ErrorMessages.NotGameHost;
                    return response;
                }

                gameInfo.Players = context.Entry(gameInfo).Collection(g => g.Players).Query().ToList();
                var game = gameManager.CreateGame(gameInfo);

                //await gameTable.DeleteGame(GameId);

                response.Success = true;
                response.Game = game;
                return response;
            }
        }

        private GameManager gameManager = GameManager.Instance;
    }
}
