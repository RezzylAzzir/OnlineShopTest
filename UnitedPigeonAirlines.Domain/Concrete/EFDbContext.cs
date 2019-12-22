using UnitedPigeonAirlines.Domain.Entities;
using System.Data.Entity;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EFDbContext : DbContext
    {

        //public EFDbContext()
        //: base("name=UnitedPigeonAirlines")
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>("MyConnection"));
        //}
        public DbSet<PigeonInOrder> PigeonsInOrders { get; set; }
        public DbSet<Pigeon> Pigeons { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders").Property(e=>e.OrderId).HasColumnName("OrderId");
            modelBuilder.Entity<PigeonInOrder>().ToTable("PigeonsInOrders");
            //modelBuilder.Entity<Order>().HasMany(t => t.Pigeons);
            modelBuilder.Entity<PigeonInOrder>().HasKey<int>(l => l.OrderId);
        }
    }
}