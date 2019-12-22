using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.EF.Repositories;
using UnitedPigeonAirlines.WebUI.Models;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;

namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class PigeonController : Controller
    {
        private EFPigeonRepository repository;
        public int pageSize = 4;
        
        
        public PigeonController(EFPigeonRepository repo)
        {
            repository = repo;
        }
        
        public FileContentResult GetImage(int pigeonId)
        {
            Pigeon pigeon = repository.GetPigeon(pigeonId);
            
                //.FirstOrDefault(g => g.PigeonId == pigeonId);

            if (pigeon != null)
            {
                return File(pigeon.ImageData, pigeon.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ViewResult List(string category, int page = 1)
        {
            PigeonListViewModel model = new PigeonListViewModel
            {
                Pigeons = repository.GetByCategory(category, page, pageSize), /*  repository.Pigeons
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(pigeon => pigeon.PigeonId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),*/
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Count(category)
                    /*
                repository.Pigeons.Count() :
                repository.Pigeons.Where(pigeon => pigeon.Ceategory == category).Count()*/
                },
                CurrentCategory = category
            };
            return View(model);
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.GetAllPigeons()
                .Select(pigeon => pigeon.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}