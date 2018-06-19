namespace CarFactoryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsumerId = c.Int(nullable: false),
                        CommodityId = c.Int(nullable: false),
                        WorkerId = c.Int(),
                        Count = c.Int(nullable: false),
                        SumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateWork = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commodities", t => t.CommodityId, cascadeDelete: true)
                .ForeignKey("dbo.Consumers", t => t.ConsumerId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId)
                .Index(t => t.ConsumerId)
                .Index(t => t.CommodityId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.Commodities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommodityName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommodityIngridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommodityId = c.Int(nullable: false),
                        IngridientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commodities", t => t.CommodityId, cascadeDelete: true)
                .ForeignKey("dbo.Ingridients", t => t.IngridientId, cascadeDelete: true)
                .Index(t => t.CommodityId)
                .Index(t => t.IngridientId);
            
            CreateTable(
                "dbo.Ingridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngridientName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageIngridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        IngridientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingridients", t => t.IngridientId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.IngridientId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Consumers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsumerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Bookings", "ConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.StorageIngridients", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageIngridients", "IngridientId", "dbo.Ingridients");
            DropForeignKey("dbo.CommodityIngridients", "IngridientId", "dbo.Ingridients");
            DropForeignKey("dbo.CommodityIngridients", "CommodityId", "dbo.Commodities");
            DropForeignKey("dbo.Bookings", "CommodityId", "dbo.Commodities");
            DropIndex("dbo.StorageIngridients", new[] { "IngridientId" });
            DropIndex("dbo.StorageIngridients", new[] { "StorageId" });
            DropIndex("dbo.CommodityIngridients", new[] { "IngridientId" });
            DropIndex("dbo.CommodityIngridients", new[] { "CommodityId" });
            DropIndex("dbo.Bookings", new[] { "WorkerId" });
            DropIndex("dbo.Bookings", new[] { "CommodityId" });
            DropIndex("dbo.Bookings", new[] { "ConsumerId" });
            DropTable("dbo.Workers");
            DropTable("dbo.Consumers");
            DropTable("dbo.Storages");
            DropTable("dbo.StorageIngridients");
            DropTable("dbo.Ingridients");
            DropTable("dbo.CommodityIngridients");
            DropTable("dbo.Commodities");
            DropTable("dbo.Bookings");
        }
    }
}
