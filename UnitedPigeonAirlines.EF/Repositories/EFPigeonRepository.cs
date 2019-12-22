using System.Collections.Generic;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using System.Linq;


namespace UnitedPigeonAirlines.EF.Repositories
{
    public class EFPigeonRepository : IPigeonRepository
    {
        
        EFDbContext context = new EFDbContext();
        
        public List<Pigeon> GetAllPigeons()
        {
            return context.Pigeons.ToList();
        }
        public Pigeon GetPigeon(int pigeonId)
        {
            return context.Pigeons.Find(pigeonId);
        }
        
        public IEnumerable<PigeonDTO> GetByOrder(Order order, Dictionary<int,int> dictonary)
        {

            // dictionary id/quanitity
            var pigIds = order.Pigeons.Select(x => x.PigeonId); //context.Orders.Single(x => x.OrderId == orderId).Pigeons.Select(x => x.PigeonId);

            return context.Pigeons.Where(x => pigIds.Contains(x.PigeonId)).ToList().Select(x => new PigeonDTO() {
                PigeonId = x.PigeonId,
                PigeonName = x.PigeonName,
                BasicPrice = x.BasicPrice,
                Category = x.Category,
                Description = x.Description,
                ImageData = x.ImageData,
                ImageMimeType = x.ImageMimeType,
                Quantity = dictonary[x.PigeonId],//vzjat' iz dictionay to x.Id
            } );
        }
        public List<Pigeon> GetByCategory(string category, int page, int pageSize)
        {
            return context.Pigeons.Where(p => category == null || p.Category == category)
            .OrderBy(pigeon => pigeon.PigeonId)
            .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }
        public void SavePigeon(Pigeon pigeon)
        {
            if (pigeon.PigeonId == 0)
                context.Pigeons.Add(pigeon);
            else
            {
                Pigeon dbEntry = context.Pigeons.Find(pigeon.PigeonId);
                if (dbEntry != null)
                {
                    dbEntry.PigeonName = pigeon.PigeonName;
                    dbEntry.Description = pigeon.Description;
                    dbEntry.BasicPrice = pigeon.BasicPrice;
                    dbEntry.Category = pigeon.Category;
                    dbEntry.ImageData = pigeon.ImageData;
                    dbEntry.ImageMimeType = pigeon.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Pigeon DeletePigeon(int pigeonId)
        {
            Pigeon dbEntry = context.Pigeons.Find(pigeonId);
            if (dbEntry != null)
            {
                context.Pigeons.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        public int Count(string category)
        {
            return context.Pigeons.Where(e => e.Category == category).Count();
        }
    }
}