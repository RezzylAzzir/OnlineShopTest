using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IFactoryCaclulation
    {
        IPriceCalculationStrategy Create(string StrategyName);
    }
}
