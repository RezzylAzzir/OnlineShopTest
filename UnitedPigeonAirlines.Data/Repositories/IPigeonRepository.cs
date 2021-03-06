﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using UnitedPigeonAirlines.Data.Entities.OrderAggregate;

namespace UnitedPigeonAirlines.Data.Repositories
{
    public interface IPigeonRepository
    {
        List<Pigeon> GetAllPigeons();
        Pigeon GetPigeon(int id);
        IEnumerable<PigeonDTO> GetByOrder(int orderId);
        List<Pigeon> GetByCategory(string category, int pageNumber, int itemsOnPage);
        int Count(string category);
        void SavePigeon(Pigeon pigeon);
        Pigeon DeletePigeon(int pigeonId);
    }
}
