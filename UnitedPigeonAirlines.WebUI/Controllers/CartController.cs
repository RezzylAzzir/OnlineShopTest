using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.WebUI.Models;

namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPigeonRepository repository;
        public CartController(IPigeonRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int PigeonId, string returnUrl)
        {
            Pigeon Pigeon = repository.Pigeons
                .FirstOrDefault(g => g.PigeonId == PigeonId);

            if (Pigeon != null)
            {
                cart.AddItem(Pigeon, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int PigeonId, string returnUrl)
        {
            Pigeon Pigeon = repository.Pigeons
                .FirstOrDefault(g => g.PigeonId == PigeonId);

            if (Pigeon != null)
            {
                cart.RemoveLine(Pigeon);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}