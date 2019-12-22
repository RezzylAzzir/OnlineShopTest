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
    public class EmailOrderProcessor : IEmailOrderProcessor
    {
        EFDbContext csontext;
        private EmailSettings emailSettings;
        EFPigeonRepository repository;
        EFOrderRepository orepostiory;
       

        public EmailOrderProcessor(EmailSettings settings, EFPigeonRepository repo, EFOrderRepository orepo, EFDbContext context)
        {
            emailSettings = settings;
            repository = repo;
            orepostiory = orepo;
            csontext = context;
        }

        public void ProcessOrder(Order order, ShippingDetails shippingInfo, Cart cart)
        {
            
            order.OrderId = csontext.Orders.OrderByDescending(x=>x.OrderId).FirstOrDefault().OrderId;
            //order.OrderId++;
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ua-UA");
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                //if (emailSettings.WriteAsFile)
                //{
                //    smtpClient.DeliveryMethod
                //        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                //    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                //    smtpClient.EnableSsl = false;
                //}

                StringBuilder body = new StringBuilder()
                    .AppendLine("New order is ready")
                    .AppendLine("---")
                    .AppendLine("Pigeons:");
                order.Pigeons = new List<PigeonInOrder>();
                foreach (var line in cart.lineCollection)
                {
                    order.Pigeons.Add(new PigeonInOrder()
                    {
                       PigeonId = line.PigeonId,
                       OrderId = order.OrderId,
                       Quantity = line.Quantity,
                    });
                    
                }
                Dictionary<int, int> dictionary = new Dictionary<int, int>();
                foreach (var pigeon in order.Pigeons)
                {
                    dictionary.Add(pigeon.PigeonId, pigeon.Quantity);
                }
                IEnumerable<PigeonDTO> pgInOrder = new PigeonDTO[] { };
                pgInOrder = repository.GetByOrder(order,dictionary);
                decimal subtotal = 0;
                foreach (var pigeon in pgInOrder)
                {

                    //PigeonDTO pigeon = new PigeonDTO(line.PigeonId);
                    subtotal += pigeon.BasicPrice * pigeon.Quantity;
                    body.AppendFormat("{0} x {1} (Summary: {2:c})",
                        pigeon.Quantity, pigeon.PigeonName, subtotal);
                }

                body.AppendFormat("Full Price: {0:c}", subtotal)
                    .AppendLine(";")
                    .AppendLine("Shippment:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("---")
                    .AppendFormat("Gift Wrapping?: {0}",
                        shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "New order shipped!",		// Тема
                                       body.ToString()); 				// Тело письма

                //if (emailSettings.WriteAsFile)
                //{
                //    mailMessage.BodyEncoding = Encoding.UTF8;
                //}


            //расскоменитровать после настройки
                //
                //smtpClient.Send(mailMessage);
            }
        }

        // move to DBOrderProcessor
        
    }
}

