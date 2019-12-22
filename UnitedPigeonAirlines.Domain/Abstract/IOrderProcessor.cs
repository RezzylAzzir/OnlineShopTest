using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Order order);
        
        
    }
}
