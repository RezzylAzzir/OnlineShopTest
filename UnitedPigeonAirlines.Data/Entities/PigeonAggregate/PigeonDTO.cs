using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedPigeonAirlines.Data.Entities.PigeonAggregate
{
    public class PigeonDTO
    {

        public int PigeonId { get; set; }
        public int Quantity { get; set; }
        public string PigeonName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal BasicPrice { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }
}
