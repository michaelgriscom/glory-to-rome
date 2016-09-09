using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Services;
using GTR.Server;
using GTR.Server.DataObjects;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using tiberService.Models;

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

            using (var context = new GtrDbContext())
            {
                const string playerId = "1";
                var player = await context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

                var game = await context.Games.FindAsync(moveSetRequest.GameId);

                var moveEntities = moveSetRequest.MoveSet.Moves.Select(
                    move => new MoveEntity()
                    {
                        DestinationId = move.DestinationId,
                        SourceId = move.SourceId,
                        CardId = move.CardId,
                        GameEntity = game,
                        OriginatingPlayer = player
                    });

                //context.MoveEntities.AddRange(moveEntities);
                var moveDomainManager = new EntityDomainManager<MoveEntity>(context, Request);
                await moveDomainManager.InsertAsync(moveEntities.First());
                await context.SaveChangesAsync();
            }

            //var DomainManager = new EntityDomainManager<MoveEntity>(context, Request);
           

            response.Success = true;
            return response;
        }

        private bool IsValidToken(int authorizationToken, string gameId)
        {
            return true;
        }
    }
}
