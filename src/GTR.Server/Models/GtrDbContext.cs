﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using GTR.Server.DataObjects;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;

namespace tiberService.Models
{
    public class GtrDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public GtrDbContext() : base(connectionStringName)
        {
        } 

        //public DbSet<TodoItem> TodoItems { get; set; }
        //public DbSet<MoveEntity> ActiveMoves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = MobileAppSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema(schema);
            }
            modelBuilder.Entity<GameEntity>().ToTable("Game");
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<PlayerEntity>()
             .HasMany<GameEntity>(s => s.Games)
             .WithMany(c => c.Players)
             .Map(cs =>
             {
                 cs.MapLeftKey("PlayerRefId");
                 cs.MapRightKey("GameRefId");
                 cs.ToTable("PlayerGames");
             });
        }

        public System.Data.Entity.DbSet<GTR.Server.DataObjects.MoveEntity> MoveEntities { get; set; }

        public System.Data.Entity.DbSet<GTR.Server.DataObjects.GameEntity> Games { get; set; }

        public DbSet<PlayerEntity> Players { get; set; }
    }

}
