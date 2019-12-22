namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
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
                .PrimaryKey(t => t.OrderId);

            
        }
        
        public override void Down()
        {
        }
    }
}
