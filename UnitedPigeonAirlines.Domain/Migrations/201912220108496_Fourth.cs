namespace UnitedPigeonAirlines.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            DropTable(
                "dbo.PigeonsInOrders");
                
        }
        
        public override void Down()
        {
        }
    }
}
