using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;

namespace UnitedPigeonAirlines.WebUI.Models
{
    public class PigeonListViewModel
    {
        public List<Pigeon> Pigeons { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}