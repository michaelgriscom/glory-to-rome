using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using GTR.Server.DataObjects;
using tiberService.Models;

namespace GTR.Server.Controllers
{
    public class GameTableController : TableController<GameEntity>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GtrDbContext context = new GtrDbContext();
            DomainManager = new EntityDomainManager<GameEntity>(context, Request);
        }

        // GET tables/GameTable
        public IQueryable<GameEntity> GetAllGame()
        {
            return Query(); 
        }

        // GET tables/GameTable/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<GameEntity> GetGame(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/GameTable/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<GameEntity> PatchGame(string id, Delta<GameEntity> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/GameTable
        public async Task<GameEntity> PostGame(GameEntity item)
        {
            GameEntity current = await InsertAsync(item);
            return current;
            //return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/GameTable/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteGame(string id)
        {
             return DeleteAsync(id);
        }

    }
}