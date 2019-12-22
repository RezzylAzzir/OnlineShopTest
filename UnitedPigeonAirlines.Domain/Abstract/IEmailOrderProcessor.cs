using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IEmailOrderProcessor
    {
        void ProcessOrder(Order order, ShippingDetails shippingDetails, Cart cart);
        
        
    }
}
