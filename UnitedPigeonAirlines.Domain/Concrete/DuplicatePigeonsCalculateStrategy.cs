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
        public DuplicatePigeonsCalculateStrategy(IPigeonRepository pigeonrepo)
        {
            pigrepo = pigeonrepo;
        }
        public decimal CalculatePrice(Cart cart,CartLine line)
        {
            int quant = line.Quantity;
            decimal Price = pigrepo.GetPigeon(line.PigeonId).BasicPrice;
            if (quant > 1)
            {
                return Price + ((Price/2) * (quant-1));
            }
            else
            {
                return Price;
            }
        }
    }
}
