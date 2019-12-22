using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();
        public List<Order> GetAllOrders()
        {
            return context.Orders.ToList();
        }

    }
}
