using UnitedPigeonAirlines.Domain.Entities;
using System.Data.Entity;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Pigeon> Pigeons { get; set; }
    }
}