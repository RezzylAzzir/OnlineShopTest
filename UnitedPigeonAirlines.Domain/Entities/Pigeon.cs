using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedPigeonAirlines.Domain.Entities
{
    public class Pigeon
    {
        public int PigeonId { get; set; }
        public string PigeonName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal BasicPrice { get; set; }
        
        }
}
