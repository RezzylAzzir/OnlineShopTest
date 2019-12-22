namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
             DropForeignKey("dbo.PigeonsInOrders", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.PigeonsInOrders", new[] { "Order_OrderId" });
        }
        
        public override void Down()
        {
        }
    }
}
