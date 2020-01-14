using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.Data.Entities.EmailAggregate;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IConfiguration
    {
        string ChosenStrategyName { get; set; }
        EmailSettings emailSettings { get; set; }

            

    }
}
