using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using GTR.Tiber.DataObjects;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using tiberService.Models;

namespace GTR.Tiber.Controllers
{
    public class MovesController : TableController<MoveEntity>
    {
        private TiberContext context;

        private GameManager gameManager = GameManager.Instance;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            this.context = new TiberContext();
            DomainManager = new EntityDomainManager<MoveEntity>(context, Request);
        }

        // GET tables/Moves/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IQueryable<MoveEntity> GetAllMoves(string gameId)
        {
            // TODO: make async
            var query = this.context.ActiveMoves.Where(move => move.GameId == gameId);
            return query;
        }

        // POST tables/Moves
        public async Task<IHttpActionResult> PostMoveAsync(MoveEntity item)
        {
            string playerId = null;
           bool moveSuccess = gameManager.MakeMove(item, playerId);


            var current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
    }
}