namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PigeonsInOrders",
                c => new
                {
                    OrderId = c.Int(nullable: false, identity: true),
                    PigeonId = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Order_OrderId = c.Int(),
                })
                .PrimaryKey(t => t.OrderId);
        }
        
        public override void Down()
        {
        }
    }
}
