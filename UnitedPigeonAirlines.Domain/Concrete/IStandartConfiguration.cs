using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Data.Entities.EmailAggregate;
using System.Web.Configuration;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class IStandartConfiguration : IConfiguration
    {
        public string ChosenStrategyName { get; set; }
        public EmailSettings emailSettings { get; set; }
        public IStandartConfiguration()
        {
            
            //ChosenStrategyName = "DefaultCalculationStrategy";
            ChosenStrategyName = WebConfigurationManager.AppSettings["ChosenStrategyName"];
            emailSettings = new EmailSettings
            {
                MailToAddress = WebConfigurationManager.AppSettings["MailToAddress"],
                MailFromAddress = WebConfigurationManager.AppSettings["MailFromAddress"],
                UseSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseSsl"]),
                Username = WebConfigurationManager.AppSettings["Username"],
                Password = WebConfigurationManager.AppSettings["Password"],
                ServerName = WebConfigurationManager.AppSettings["ServerName"],
                ServerPort = Convert.ToInt32(WebConfigurationManager.AppSettings["ServerPort"]),
                WriteAsFile = Convert.ToBoolean(WebConfigurationManager.AppSettings["WriteAsFile"]),
                FileLocation = WebConfigurationManager.AppSettings["FileLocation"],
            };
        }
    }
}
