using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;
using UnitedPigeonAirlines.Data.Entities.OrderAggregate;
using System.Data.Entity;

namespace UnitedPigeonAirlines.EF.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {

        public List<Order> GetAllOrders()
        {
            using (EFDbContext context = new EFDbContext())
            {

                return context.Orders.Include(x=>x.Pigeons).ToList();
            }
        }
        public void SaveOrder(Order order)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
    }
}
