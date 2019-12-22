using System.Net;
using System.Net.Mail;
using System.Text;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Domain.Entities;
using UnitedPigeonAirlines.Data.Entities;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class DBOrderProcessor:IDBOrderProcessor
    {

        EFDbContext context = new EFDbContext();
        public void SaveOrder(Cart cart, ShippingDetails shippingInfo)
        {
            Order dbEntry = new Order();
            
            
            dbEntry = context.Orders.Add(dbEntry);
            dbEntry.Name = shippingInfo.Name;
            //dbEntry.OrderId = shippingInfo.ShippingId;
            dbEntry.GiftWrap = shippingInfo.GiftWrap;
            dbEntry.City = shippingInfo.City;
            dbEntry.Country = shippingInfo.Country;
            dbEntry.Line1 = shippingInfo.Line1;
            dbEntry.Line2 = shippingInfo.Line2;
            dbEntry.Line3 = shippingInfo.Line3;
            foreach (var line in cart.Lines)
            {


                PigeonInOrder pigeondbEntry = new PigeonInOrder();
                pigeondbEntry.OrderId = context.Orders.OrderByDescending(x => x.OrderId).FirstOrDefault().OrderId;
                pigeondbEntry.PigeonId = line.PigeonId;
                pigeondbEntry.Quantity = line.Quantity;
                pigeondbEntry = context.PigeonsInOrders.Add(pigeondbEntry);
                context.SaveChanges();
                //PigeonDTO pigeon = new PigeonDTO(line.PigeonId);
                //var subtotal = pigeon.BasicPrice * line.Quantity;
                //dbEntry.SummaryPrice = dbEntry.SummaryPrice + subtotal;
                //dbEntry.PigeonsIds = dbEntry.PigeonsIds + "  " + pigeon.PigeonId;
            }



            context.SaveChanges();
        }
    }

}
