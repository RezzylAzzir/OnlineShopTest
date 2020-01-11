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
using System.Collections.Generic;


namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        
        private EmailSettings emailSettings;
        EFPigeonRepository repository;

       

        public EmailOrderProcessor(EmailSettings settings, EFPigeonRepository repo)
        {
            emailSettings = settings;
            repository = repo;
        }

        public void ProcessOrder(Order order)
        {
            
            
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ua-UA");
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

             
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
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "New order shipped!",		// Тема
                                       body.ToString()); 				// Тело письма

            //расскоментировать после настройки
                //
                //smtpClient.Send(mailMessage);
            }
        }
        
    }
}

