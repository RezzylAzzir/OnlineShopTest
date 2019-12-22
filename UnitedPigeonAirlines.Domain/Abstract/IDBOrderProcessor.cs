using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IDBOrderProcessor
    {
        void SaveOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
