using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class DefaultCalculationStrategy:IPriceCalculationStrategy
    {

        private IPigeonRepository pigrepo;
        public DefaultCalculationStrategy(IPigeonRepository pigeonrepo)
        {
            pigrepo = pigeonrepo;
        }
        public decimal CalculatePrice(Cart cart,CartLine line)
        {
            int quant = line.Quantity;
            decimal Price = pigrepo.GetPigeon(line.PigeonId).BasicPrice;
            if (quant > 1)
            {
                return Price  * quant;
            }
            else
            {
                return Price;
            }
        }
    }
}
