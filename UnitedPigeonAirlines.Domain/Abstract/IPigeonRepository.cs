using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Entities;

namespace UnitedPigeonAirlines.Domain.Abstract
{
    public interface IPigeonRepository
    {
        IEnumerable<Pigeon> Pigeons { get; }
    }
}
