#region

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using GTR.Server.DataObjects;
using Microsoft.Azure.Mobile.Server;
using tiberService.Models;

#endregion

namespace GTR.Server.Controllers
{
    public class LobbyTable
    {
        private static readonly GtrDbContext context;

        static LobbyTable ()
        {
            context = new GtrDbContext();
    }

    public LobbyTable()
        {
        }

        public IQueryable<GameEntity> GetAllLobbyGames(HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<GameEntity>(context, request);

            return DomainManager.Query();
        }

        public async Task<SingleResult<GameEntity>> GetLobbyGame(string id, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<GameEntity>(context, request);

            return await DomainManager.LookupAsync(id);
        }

        public Task<GameEntity> UpdateLobbyGame(string id, Delta<GameEntity> patch, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<GameEntity>(context, request);
            return DomainManager.UpdateAsync(id, patch);
        }

        public async Task<GameEntity> AddLobbyGame(GameEntity item, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<GameEntity>(context, request);

            GameEntity current = await DomainManager.InsertAsync(item);
            return current;
        }

        public async Task DeleteLobbyGame(string id, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<GameEntity>(context, request);

            await DomainManager.DeleteAsync(id);
        }
    }
}