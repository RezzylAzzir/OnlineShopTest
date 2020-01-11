using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;

namespace UnitedPigeonAirlines.Data.Entities.OrderAggregate
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
        public decimal SummaryPrice { get; set; }
        public List<PigeonInOrder> Pigeons { get; set; }
    }
}
