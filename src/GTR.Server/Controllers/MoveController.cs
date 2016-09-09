using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using GTR.Core.Marshalling.DTO;
using GTR.Server.DataObjects;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables;
using tiberService.Models;

namespace GTR.Server.Controllers
{
    [MobileAppController]
    public class MoveController : ApiController
    {
        GtrDbContext context = new GtrDbContext();
        private EntityDomainManager<MoveEntity> domainManager;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
           domainManager = new EntityDomainManager<MoveEntity>(context, Request);
        }

        //public ApiServices Services { get; set; }

        public IEnumerable<ExecutedMoveSerialization> GetMoves(string gameId)
        {
           var gameEngine = GameManager.Instance.GetGameInfo(gameId);

            return gameEngine.MoveHistory.Moves;
        }
    }
}
