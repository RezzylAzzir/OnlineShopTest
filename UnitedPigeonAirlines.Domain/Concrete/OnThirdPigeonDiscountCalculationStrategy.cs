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
    public class OnThirdPigeonDiscountCalculationStrategy: IPriceCalculationStrategy
    {
        private IPigeonRepository pigrepo;
        public OnThirdPigeonDiscountCalculationStrategy(IPigeonRepository pigeonRepository)
        {
            pigrepo = pigeonRepository;
        }
        
        public decimal CalculatePrice(Cart cart, CartLine line)
        {
            int quant = line.Quantity;
            int i = cart.Lines.Count();
            int j = cart.Lines.FindIndex(x => x.PigeonId == line.PigeonId);
            decimal CurrentPigeonPrice = pigrepo.GetPigeon(cart.Lines[j].PigeonId).BasicPrice;
            if (i > 2 && (1 + j) % 3 == 0)
            {
                decimal FirstPigeonPrice = pigrepo.GetPigeon(cart.Lines[j - 2].PigeonId).BasicPrice;
                decimal SecondPigeonPrice = pigrepo.GetPigeon(cart.Lines[j - 1].PigeonId).BasicPrice;
                if (FirstPigeonPrice > CurrentPigeonPrice && SecondPigeonPrice > CurrentPigeonPrice)
                {
                    return CurrentPigeonPrice / 2 + (CurrentPigeonPrice * (quant - 1));
                }
                else
                {
                    return CurrentPigeonPrice * quant;
                }
            }
            else
            {
                return CurrentPigeonPrice * quant;
            }

        }
    }
}
