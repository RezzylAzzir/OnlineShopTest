using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.WebUI.Models;
using UnitedPigeonAirlines.EF.Repositories;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;


namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class CartController : Controller
    {
        private EFPigeonRepository repository;
        private EFOrderRepository orderRepository;
        private IOrderProcessor orderProcessor;
        public CartController(EFPigeonRepository repo,EFOrderRepository orepo, IOrderProcessor processor/*, IDBOrderProcessor dBOrderProcessor*/)
        {
            repository = repo;
            orderRepository = orepo;
            orderProcessor = processor;
            //dbProcessor = dBOrderProcessor;
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
                cart.FillOrder(order);
                order.City = shippingDetails.City;
                order.Country = shippingDetails.Country;
                order.GiftWrap = shippingDetails.GiftWrap;
                order.Line1 = shippingDetails.Line1;
                order.Line2 = shippingDetails.Line2;
                order.Line3 = shippingDetails.Line3;
                order.Name = shippingDetails.Name;
                orderProcessor.ProcessOrder(order);
                orderRepository.SaveOrder(order);
                //dbProcessor.SaveOrder(cart, shippingDetails);
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