using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;

namespace UnitedPigeonAirlines.WebUI.Models
{
    
    public class PigeonInCartDTO {
        
        public PigeonViewModel Pigeon {get;set;}

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public PigeonInCartDTO(CartLine cartLine, Pigeon pigeon) {
            Pigeon = new PigeonViewModel(pigeon);
            Quantity = cartLine.Quantity;
            Price = 0;
        }
    }

    public class PigeonViewModel
    {

        
       
        [HiddenInput(DisplayValue = false)]
        public int PigeonId { get; set; }
        [Display(Name = "Pigeon Name")]
        [Required(ErrorMessage = "Please, enter pigeon name")]
        public string PigeonName { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Pigeon Description")]
        [Required(ErrorMessage = "Please, enter pigeon description")]
        public string Description { get; set; }
        [Display(Name = "Pigeons Category")]
        [Required(ErrorMessage = "Please, enter pigeon category")]
        public string Category { get; set; }
        [Display(Name = "Price (UAH)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter legit price")]
        public decimal BasicPrice { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public PigeonViewModel() {
        }
        

        public PigeonViewModel(Pigeon pigeon)
        {
            PigeonId = pigeon.PigeonId;
            PigeonName = pigeon.PigeonName;
            Description = pigeon.Description;
            Category = pigeon.Category;
            BasicPrice = pigeon.BasicPrice;
            ImageData = pigeon.ImageData;
            ImageMimeType = pigeon.ImageMimeType;

        }
        public Pigeon ToPigeon()
        {
            
                Pigeon pigeon = new Pigeon();
                pigeon.PigeonId = PigeonId;
                pigeon.PigeonName = PigeonName;
                pigeon.Description = Description;
                pigeon.Category = Category;
                pigeon.BasicPrice = BasicPrice;
                pigeon.ImageData = ImageData;
                pigeon.ImageMimeType = ImageMimeType;
                return pigeon;
            
        }

    }
}
