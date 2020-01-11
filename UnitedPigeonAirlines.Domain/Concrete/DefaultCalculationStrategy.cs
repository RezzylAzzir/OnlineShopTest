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
        public DefaultCalculationStrategy(IPigeonRepository pigeonRepository)
        {
            pigrepo = pigeonRepository;
            
        }
        public decimal CalculatePrice(Cart cart,CartLine line)
        {
            if (line.Quantity > 1)
            {
                return pigrepo.GetPigeon(line.PigeonId).BasicPrice * line.Quantity;
            }
            else
            {
                return pigrepo.GetPigeon(line.PigeonId).BasicPrice;
            }
        }
    }
}
