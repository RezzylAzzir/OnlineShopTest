using System.Data.Entity;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;

namespace UnitedPigeonAirlines.EF
{
    public class EFDbContext : DbContext
    {
        public DbSet<PigeonInOrder> PigeonsInOrders { get; set; }
        public DbSet<Pigeon> Pigeons { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders").Property(e=>e.OrderId).HasColumnName("OrderId");
            modelBuilder.Entity<PigeonInOrder>().ToTable("PigeonsInOrders");
        }
    }
}