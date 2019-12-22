namespace UnitedPigeonAirlines.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit1 : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.PigeonsInOrders",
                c => new
                    {
                        PigeonInOrderId = c.Int(nullable: false, identity: true),
                        PigeonId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.PigeonInOrderId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            
            
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
