using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Entities;


namespace UnitedPigeonAirlines.Data.Repositories
{
    public interface IPigeonRepository
    {
        //IEnumerable<Pigeon> Pigeons { get; }
        List<Pigeon> GetAllPigeons();
        Pigeon GetPigeon(int id);
        List<Pigeon> GetByCategory(string category, int pageNumber, int itemsOnPage);
        //List<string> GetAllCategories();
        /// <summary>
        /// return 
        /// count by category or total pigeons count if no category specified
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        int Count(string category);
        void SavePigeon(Pigeon pigeon);
        Pigeon DeletePigeon(int pigeonId);
    }
}
