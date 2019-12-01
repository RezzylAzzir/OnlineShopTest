using System.Collections.Generic;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Domain.Abstract;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EFPigeonRepository : IPigeonRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Pigeon> Pigeons
        {
            get { return context.Pigeons; }
        }
    }
}