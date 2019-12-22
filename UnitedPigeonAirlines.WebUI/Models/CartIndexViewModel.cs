using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.WebUI.Models
{
    public class CartIndexViewModel
    {                                   
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public List<PigeonInCartDTO> PigeonsInCart { get; set; }
    }
}