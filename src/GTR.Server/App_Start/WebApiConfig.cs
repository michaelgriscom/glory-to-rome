using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using tiberService.Models;
using GTR.Core.Util;

namespace tiberService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            JsonNetConfiguration.ConfigureJsonNet();
            Database.SetInitializer(new DbInitializer());
        }
    }

    public class DbInitializer : ClearDatabaseSchemaIfModelChanges<GtrDbContext>
    {
   
    }
}

