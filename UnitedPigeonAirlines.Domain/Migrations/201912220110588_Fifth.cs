namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PigeonsInOrders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PigeonId = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    OrderId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
        }
    }
}
