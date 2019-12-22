using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.EF.Repositories;

namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class NavController : Controller
    {
        private EFPigeonRepository repository;

        public NavController(EFPigeonRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = repository.GetAllPigeons()
                .Select(pigeon => pigeon.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}