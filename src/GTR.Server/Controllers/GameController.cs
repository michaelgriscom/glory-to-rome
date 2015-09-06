using System;
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

            string playerId = GetPlayerId(moveSetRequest.AuthorizationToken, moveSetRequest.GameId);

            using (var context = new GtrDbContext())
            {
                var game = await context.Games.FindAsync(moveSetRequest.GameId);
                var DomainManager = new EntityDomainManager<MoveEntity>(context, Request);

                var moveEntities = moveSetRequest.MoveSet.Moves.Select(
                    move => new MoveEntity()
                    {
                        DestinationId = move.DestinationId,
                        SourceId = move.SourceId,
                        CardId = move.CardId,
                        GameEntity = game,
                    }
                    );
            
                //context.MoveEntities.AddRange(moveEntities);
                await DomainManager.InsertAsync(moveEntities.First());
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

        private string GetPlayerId(int authorizationToken, string gameId)
        {
            return "1";
        }
    }
}
