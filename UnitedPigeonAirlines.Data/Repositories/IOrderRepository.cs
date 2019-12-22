using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Data.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
    }
}
