﻿#region

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using GTR.Tiber.DataObjects;
using Microsoft.Azure.Mobile.Server;
using tiberService.Models;

#endregion

namespace GTR.Tiber.Controllers
{
    public class LobbyTable
    {
        private readonly TiberContext context;

        public LobbyTable()
        {
            context = new TiberContext();
        }

        public IQueryable<LobbyGame> GetAllLobbyGames(HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<LobbyGame>(context, request);

            return DomainManager.Query();
        }

        public async Task<SingleResult<LobbyGame>> GetLobbyGame(string id, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<LobbyGame>(context, request);

            return await DomainManager.LookupAsync(id);
        }

        public Task<LobbyGame> UpdateLobbyGame(string id, Delta<LobbyGame> patch, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<LobbyGame>(context, request);
            return DomainManager.UpdateAsync(id, patch);
        }

        public async Task<LobbyGame> AddLobbyGame(LobbyGame item, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<LobbyGame>(context, request);

            LobbyGame current = await DomainManager.InsertAsync(item);
            return current;
        }

        public async Task DeleteLobbyGame(string id, HttpRequestMessage request)
        {
            var DomainManager = new EntityDomainManager<LobbyGame>(context, request);

            await DomainManager.DeleteAsync(id);
        }
    }
}