using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Entities;

namespace UnitedPigeonAirlines.Domain.Entities
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

        public PigeonDTO()
        {
        }
        //public Dictionary<int, int> pipigeons { get; set; }
        //public PigeonDTO2(Pigeon pigeon, PigeonInOrder pigeonInOtder)
        //{
        //    PigeonId = pigeon.PigeonId;
        //    PigeonName = pigeon.PigeonName;
        //    Description = pigeon.Description;
        //    Category = pigeon.Category;
        //    BasicPrice = pigeon.BasicPrice;
        //    ImageData = pigeon.ImageData;
        //    ImageMimeType = pigeon.ImageMimeType;
        //    Quantity = pigeonInOtder.Quantity;

        //}
    }
}
