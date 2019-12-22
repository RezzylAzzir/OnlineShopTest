namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Line1 = c.String(),
                        Line2 = c.String(),
                        Line3 = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        GiftWrap = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.PigeonsInOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        PigeonId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.Pigeons",
                c => new
                    {
                        PigeonId = c.Int(nullable: false, identity: true),
                        PigeonName = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                        BasicPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.PigeonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PigeonsInOrders", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.PigeonsInOrders", new[] { "Order_OrderId" });
            DropTable("dbo.Pigeons");
            DropTable("dbo.PigeonsInOrders");
            DropTable("dbo.Orders");
        }
    }
}
