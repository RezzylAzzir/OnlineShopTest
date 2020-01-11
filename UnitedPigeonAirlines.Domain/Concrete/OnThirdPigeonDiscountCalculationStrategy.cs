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
    //Скидка на третьего голубя 50% при покупке двух дорогих до этого, акция работает один раз на каждые 3 вида голубей (2 дорогих и 1 дешевый)
    public class OnThirdDiscountCalculationStrategy: IPriceCalculationStrategy
    {
        private IPigeonRepository pigrepo;
        public OnThirdDiscountCalculationStrategy(IPigeonRepository pigeonRepository)
        {
            pigrepo = pigeonRepository;
        }
        
        public decimal CalculatePrice(Cart cart, CartLine line)
        {
            
                int i = cart.Lines.Count();
                int j = cart.Lines.FindIndex(x=>x.PigeonId==line.PigeonId);
                if (i>2 && (1+j) % 3==0)
                {

                    if (pigrepo.GetPigeon(cart.Lines[j - 1].PigeonId).BasicPrice > pigrepo.GetPigeon(cart.Lines[j].PigeonId).BasicPrice && pigrepo.GetPigeon(cart.Lines[j - 2].PigeonId).BasicPrice > pigrepo.GetPigeon(cart.Lines[j].PigeonId).BasicPrice)
                    {
                        return pigrepo.GetPigeon(line.PigeonId).BasicPrice / 2 + (pigrepo.GetPigeon(line.PigeonId).BasicPrice * (line.Quantity - 1));
                    }
                    else
                    {
                        return pigrepo.GetPigeon(line.PigeonId).BasicPrice * line.Quantity;
                    }
                }
                else
                {
                    return pigrepo.GetPigeon(line.PigeonId).BasicPrice * line.Quantity;
                }

        }
    }
}
