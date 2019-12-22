using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Data.Entities.PigeonAggregate
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
        //public decimal SummaryPrice { get; set; }
        //public string PigeonsIds { get; set; }

        public List<PigeonInOrder> Pigeons { get; set; }
        
    }
}
