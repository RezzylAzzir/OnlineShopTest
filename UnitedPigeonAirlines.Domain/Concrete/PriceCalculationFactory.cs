using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Data.Repositories;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class PriceCalculationFactory : IFactoryCaclulation
    {
        IPigeonRepository pigeonRepository;
        public PriceCalculationFactory(IPigeonRepository pigeonRepo)
        {
            pigeonRepository = pigeonRepo;
        }
        public IPriceCalculationStrategy Create(string StrategyName)
        {
            IPriceCalculationStrategy priceCalculation = null;
            switch (StrategyName)
            {
                case "DefaultCalculationStrategy":
                    
                    priceCalculation = new DefaultCalculationStrategy(pigeonRepository);
                    break;
                case "DuplicatePigeonCalculationStrategy":
                    priceCalculation = new DuplicatePigeonsCalculateStrategy(pigeonRepository);
                    break;
                case "OnThirdPigeonDiscountCalculation":
                    priceCalculation = new OnThirdDiscountCalculationStrategy(pigeonRepository);
                    break;
            }
            return priceCalculation;
        }
    }
}
