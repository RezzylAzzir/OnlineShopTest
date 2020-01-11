using System.Collections.Generic;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using UnitedPigeonAirlines.Data.Entities.OrderAggregate;
using System.Linq;
using System.Data.Entity;


namespace UnitedPigeonAirlines.EF.Repositories
{
    public class EFPigeonRepository : IPigeonRepository
    {
        
        
        
        public List<Pigeon> GetAllPigeons()
        {

            
            using (EFDbContext context = new EFDbContext())
            {
                return context.Pigeons.ToList();
            }
                
            
        }
        public Pigeon GetPigeon(int pigeonId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                return context.Pigeons.Find(pigeonId);
            }
        }
        
        public IEnumerable<PigeonDTO> GetByOrder(int orderId)
        {

            using (EFDbContext context = new EFDbContext())
            {
                var pigs = context.PigeonsInOrders.Where(x=>x.OrderId==orderId).ToDictionary(x => x.PigeonId, x => x.Quantity);

                return context.Pigeons.Where(x => pigs.Keys.Contains(x.PigeonId)).ToList().Select(x => new PigeonDTO()
                {
                    PigeonId = x.PigeonId,
                    PigeonName = x.PigeonName,
                    BasicPrice = x.BasicPrice,
                    Category = x.Category,
                    Description = x.Description,
                    ImageData = x.ImageData,
                    ImageMimeType = x.ImageMimeType,
                    Quantity = pigs[x.PigeonId],
                });
            }
        }
        public List<Pigeon> GetByCategory(string category, int page, int pageSize)
        {
            using (EFDbContext context = new EFDbContext())
            {
                return context.Pigeons.Where(p => category == null || p.Category == category)
                .OrderBy(pigeon => pigeon.PigeonId)
                .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
        }
        public void SavePigeon(Pigeon pigeon)
        {
            using (EFDbContext context = new EFDbContext())
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
        }
        public Pigeon DeletePigeon(int pigeonId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                Pigeon dbEntry = context.Pigeons.Find(pigeonId);
                if (dbEntry != null)
                {
                    context.Pigeons.Remove(dbEntry);
                    context.SaveChanges();
                }
                return dbEntry;
            }
        }
        public int Count(string category)
        {
            using (EFDbContext context = new EFDbContext())
            {
                return context.Pigeons.Where(e => e.Category == category).Count();
            }
        }
    }
}