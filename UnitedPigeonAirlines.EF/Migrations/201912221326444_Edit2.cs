namespace UnitedPigeonAirlines.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PigeonsInOrders", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.PigeonsInOrders", new[] { "Order_OrderId" });
            RenameColumn(table: "dbo.PigeonsInOrders", name: "Order_OrderId", newName: "OrderId");
            AlterColumn("dbo.PigeonsInOrders", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.PigeonsInOrders", "OrderId");
            AddForeignKey("dbo.PigeonsInOrders", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PigeonsInOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.PigeonsInOrders", new[] { "OrderId" });
            AlterColumn("dbo.PigeonsInOrders", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.PigeonsInOrders", name: "OrderId", newName: "Order_OrderId");
            CreateIndex("dbo.PigeonsInOrders", "Order_OrderId");
            AddForeignKey("dbo.PigeonsInOrders", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}
