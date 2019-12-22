namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            DropTable("PigeonsInOrders");
        }
        
        public override void Down()
        {
        }
    }
}
