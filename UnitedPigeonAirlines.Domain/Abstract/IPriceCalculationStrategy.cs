using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IPriceCalculationStrategy
    {
        decimal CalculatePrice(Cart cart,CartLine line);
    }   
}
