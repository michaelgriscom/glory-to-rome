using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using GTR.Core.Marshalling.DTO;
using Microsoft.Azure.Mobile.Server;
using GTR.Server.DataObjects;
using tiberService.Models;

namespace GTR.Server.Controllers
{
    public class MoveEntityController : TableController<MoveEntity>
    {
        GtrDbContext context = new GtrDbContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<MoveEntity>(context, Request);
        }

        //// GET tables/MoveEntity
        //public IQueryable<MoveEntity> GetAllMoveEntity()
        //{
        //    return Query(); 
        //}

        //// GET tables/MoveEntity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public SingleResult<MoveEntity> GetMoveEntity(string id)
        //{
        //    return Lookup(id);
        //}

        [Route("api/move")]
        public async Task<IHttpActionResult> PostMove(MoveSetRequest moveSetRequest)
        {
            var game = await context.Games.FindAsync(moveSetRequest.GameId);
            if (game == null)
            {
                return BadRequest();
            }

            foreach (var move in moveSetRequest.MoveSet.Moves)
            {
                context.MoveEntities.Add(new MoveEntity()
                {
                    DestinationId = move.DestinationId,
                    SourceId = move.SourceId,
                    CardId = move.CardId,
                    GameEntity = game
                });
            }

            await context.SaveChangesAsync();

            return Ok();
        }

        //[Route("api/move")]
        //public IQueryable<MoveSerialization> GetMoves(string gameId)
        //{
        //    return Query().Where(x => x.Game.Id == gameId).Select(x =>
        //    new MoveSerialization()
        //    {
        //        CardId = x.CardId,
        //        DestinationId = x.DestinationId,
        //        SourceId = x.SourceId
        //    }
        //    );
        //}

        //[Route("api/move")]
        //public async Task<IHttpActionResult> GetMove(string gameId)
        //{
        //    var game = await context.Games.FindAsync(moveSetRequest.GameId);
        //    if (game == null)
        //    {
        //        return BadRequest();
        //    }

        //    foreach (var move in moveSetRequest.MoveSet.Moves)
        //    {
        //        context.MoveEntities.Add(new MoveEntity()
        //        {
        //            DestinationId = move.DestinationId,
        //            SourceId = move.SourceId,
        //            CardId = move.CardId,
        //            Game = game
        //        });
        //    }

        //    await context.SaveChangesAsync();

        //    return Ok();
        //}

        //// POST tables/MoveEntity
        //public async Task<IHttpActionResult> PostMoveEntity(MoveEntity item)
        //{
        //    MoveEntity current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // GET tables/MoveEntity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public IQueryable<MoveSerialization> GetMoves(string gameId)
        //{
        //    return Query().Where(x => x.Game.Id == gameId).Select(x =>
        //    new MoveSerialization()
        //    {
        //        CardId = x.CardId,
        //        DestinationId =  x.DestinationId,
        //        SourceId = x.SourceId
        //    }
        //    );
        //}
    }
}