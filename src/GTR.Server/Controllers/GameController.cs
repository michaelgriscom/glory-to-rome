using System;
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

            GtrDbContext context = new GtrDbContext();


            var DomainManager = new EntityDomainManager<MoveEntity>(context, Request);
            var game = await context.Games.FindAsync(moveSetRequest.GameId);

            foreach (var move in moveSetRequest.MoveSet.Moves)
            {
                var moveEntity = new MoveEntity()
                {
                    DestinationId = move.DestinationId,
                    SourceId = move.SourceId,
                    CardId = move.CardId,
                    GameEntity = game
                };
                await DomainManager.InsertAsync(moveEntity);
            }

            await context.SaveChangesAsync();

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
