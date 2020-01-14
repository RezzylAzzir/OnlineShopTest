using System.Net;
using System.Net.Mail;
using System.Text;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Data.Entities.OrderAggregate;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using System.Linq;
using UnitedPigeonAirlines.EF.Repositories;
using System.Data.Entity;
using UnitedPigeonAirlines.Data.Repositories;
using System.Collections.Generic;


namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        
        private IConfiguration configuration;
        private IPigeonRepository repository;

       

        public EmailOrderProcessor(IConfiguration conf, IPigeonRepository repo)
        {
            configuration = conf;
            repository = repo;
        }

        public void ProcessOrder(Order order)
        {
            
            
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ua-UA");
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = configuration.emailSettings.UseSsl;
                smtpClient.Host = configuration.emailSettings.ServerName;
                smtpClient.Port = configuration.emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(configuration.emailSettings.Username, configuration.emailSettings.Password);

             
                StringBuilder body = new StringBuilder()
                    .AppendLine("New order is ready")
                    .AppendLine("---")
                    .AppendLine("Pigeons:");

                var pgInOrder = repository.GetByOrder(order.OrderId);
                decimal subtotal = 0;
                foreach (var pigeon in pgInOrder)
                {
                    subtotal += pigeon.BasicPrice * pigeon.Quantity;
                    body.AppendFormat("{0} x {1} (Summary: {2:c})",
                        pigeon.Quantity, pigeon.PigeonName, subtotal);
                }

                body.AppendFormat("Full Price: {0:c}", subtotal)
                    .AppendLine(";")
                    .AppendLine("Shippment:")
                    .AppendLine(order.Name)
                    .AppendLine(order.Line1)
                    .AppendLine(order.Line2 ?? "")
                    .AppendLine(order.Line3 ?? "")
                    .AppendLine(order.City)
                    .AppendLine(order.Country)
                    .AppendLine("---")
                    .AppendFormat("Gift Wrapping?: {0}",
                        order.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                                       configuration.emailSettings.MailFromAddress,	// От кого
                                       configuration.emailSettings.MailToAddress,		// Кому
                                       "New order shipped!",		// Тема
                                       body.ToString()); 				// Тело письма

            //расскоментировать после настройки
                //
                //smtpClient.Send(mailMessage);
            }
        }
        
    }
}

