using System.ComponentModel.DataAnnotations;
using UnitedPigeonAirlines.Data.Entities;
using System.Web.Mvc;

namespace UnitedPigeonAirlines.WebUI.Models
{
    public class ShippingDetails
    {
        //[HiddenInput(DisplayValue = false)]
        //public int ShippingId { get; set; }
        [Required(ErrorMessage = "Type your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Input first address")]
        [Display(Name = "First addres")]
        public string Line1 { get; set; }
        [Display(Name = "Second addres")]
        public string Line2 { get; set; }
        [Display(Name = "Third addres")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Input proper city name")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Input proper country name")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
        //Order Order { get; set; }
    }
}