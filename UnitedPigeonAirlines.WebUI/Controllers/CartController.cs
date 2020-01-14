using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.WebUI.Models;
using UnitedPigeonAirlines.EF.Repositories;
using UnitedPigeonAirlines.Data.Entities.OrderAggregate;
using UnitedPigeonAirlines.Data.Entities.CartAggregate;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using UnitedPigeonAirlines.Data.Repositories;


namespace UnitedPigeonAirlines.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPigeonRepository repository;
        private IOrderRepository orderRepository;
        private IOrderProcessor orderProcessor;
        private IFactoryCaclulation priceCalcFactory;
        private IConfiguration configuration;
        


        public CartController(IPigeonRepository repo,IOrderRepository orepo, IOrderProcessor processor, IFactoryCaclulation priceCalculation, IConfiguration config)
        {
            repository = repo;
            orderRepository = orepo;
            orderProcessor = processor;
            priceCalcFactory = priceCalculation;
            configuration = config;
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

            public ViewResult Index(Cart cart, string returnUrl)
        {

            CartIndexViewModel indexViewModel = new CartIndexViewModel();
            indexViewModel.Cart = cart;
            indexViewModel.ReturnUrl = returnUrl;
            indexViewModel.PigeonsInCart = cart.Lines.Select(x => new PigeonInCartDTO(x, repository.GetPigeon(x.PigeonId))).ToList();
            foreach(var pig in indexViewModel.PigeonsInCart)
            {
                pig.Price = priceCalcFactory.Create(configuration.ChosenStrategyName).CalculatePrice(cart, cart.Lines.Find(x => x.PigeonId == pig.Pigeon.PigeonId));
                
            }
            return View(indexViewModel);
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
        public ViewResult Checkout( Cart cart, ShippingDetails shippingDetails)
        {
            CartIndexViewModel indexViewModel = new CartIndexViewModel();
            decimal Subtotal = 0;
            indexViewModel.PigeonsInCart = cart.Lines.Select(x => new PigeonInCartDTO(x, repository.GetPigeon(x.PigeonId))).ToList();
            foreach (var pig in indexViewModel.PigeonsInCart)
            {
                pig.Price = priceCalcFactory.Create(configuration.ChosenStrategyName).CalculatePrice(cart, cart.Lines.Find(x => x.PigeonId == pig.Pigeon.PigeonId));
                Subtotal += pig.Price;
            }
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                Order order = new Order();
                cart.FillOrder(order);
                order.City = shippingDetails.City;
                order.Country = shippingDetails.Country;
                order.GiftWrap = shippingDetails.GiftWrap;
                order.Line1 = shippingDetails.Line1;
                order.Line2 = shippingDetails.Line2;
                order.Line3 = shippingDetails.Line3;
                order.Name = shippingDetails.Name;
                order.SummaryPrice = Subtotal;
                orderRepository.SaveOrder(order);
                orderProcessor.ProcessOrder(order);
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