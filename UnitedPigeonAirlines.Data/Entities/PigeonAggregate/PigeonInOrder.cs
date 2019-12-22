using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedPigeonAirlines.Data.Entities.PigeonAggregate
{
    public class PigeonInOrder
    {
        public int PigeonInOrderId { get; set; }
        public PigeonInOrder()
        {
        }
        public int PigeonId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
