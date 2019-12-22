using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedPigeonAirlines.Data.Entities.CartAggregate
{
    public class CartLine
    {
        // should be pigeonId, because Cart is separate aggregate annd Pigeon is separate aggregate
        public int PigeonId { get; set; }
        public int Quantity { get; set; }
    }
}