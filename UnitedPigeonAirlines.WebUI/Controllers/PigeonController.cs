using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.WebUI.Models;

namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class PigeonController : Controller
    {
        private IPigeonRepository repository;
        public int pageSize = 2;
        public PigeonController(IPigeonRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string category, int page = 1)
        {
            PigeonListViewModel model = new PigeonListViewModel
            {
                Pigeons = repository.Pigeons
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(pigeon => pigeon.PigeonId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                repository.Pigeons.Count() :
                repository.Pigeons.Where(pigeon => pigeon.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Pigeons
                .Select(pigeon => pigeon.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}