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
    //Добавляет скидку 50% на каждого дополнительного голубя того же вида
    public class DuplicatePigeonsCalculateStrategy:IPriceCalculationStrategy
    {
        private IPigeonRepository pigrepo;
        public DuplicatePigeonsCalculateStrategy(IPigeonRepository pigeonRepository)
        {
            pigrepo = pigeonRepository;

        }
        public decimal CalculatePrice(Cart cart,CartLine line)
        {
            if (line.Quantity > 1)
            {
                return pigrepo.GetPigeon(line.PigeonId).BasicPrice + ((pigrepo.GetPigeon(line.PigeonId).BasicPrice/2) * (line.Quantity-1));
            }
            else
            {
                return pigrepo.GetPigeon(line.PigeonId).BasicPrice;
            }
        }
    }
}
