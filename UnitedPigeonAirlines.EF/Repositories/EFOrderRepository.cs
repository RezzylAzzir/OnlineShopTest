using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;

namespace UnitedPigeonAirlines.EF.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();
        public List<Order> GetAllOrders()
        {
            return context.Orders.ToList();
        }
        public void SaveOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}
