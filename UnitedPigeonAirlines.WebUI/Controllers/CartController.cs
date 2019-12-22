using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.WebUI.Models;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.Data.Entities;


namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class CartController : Controller
    {
        private EFPigeonRepository repository;
        private IEmailOrderProcessor orderProcessor;
        private IDBOrderProcessor dbProcessor;
        public CartController(EFPigeonRepository repo, IEmailOrderProcessor processor, IDBOrderProcessor dBOrderProcessor)
        {
            repository = repo;
            orderProcessor = processor;
            dbProcessor = dBOrderProcessor;
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

            public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                PigeonsInCart = cart.Lines.Select(x => new PigeonInCartDTO(x,repository.GetPigeon(x.PigeonId))).ToList(),
                
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int PigeonId, string returnUrl)
        {
            Pigeon Pigeon = repository.GetAllPigeons()
                .FirstOrDefault(g => g.PigeonId == PigeonId);

            if (Pigeon != null)
            {
                cart.AddItem(Pigeon, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int PigeonId, string returnUrl)
        {
            Pigeon Pigeon = repository.GetAllPigeons()
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
        [HttpPost]
        public ViewResult Checkout(Order order, Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                
                orderProcessor.ProcessOrder(order, shippingDetails,cart);
                dbProcessor.SaveOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}