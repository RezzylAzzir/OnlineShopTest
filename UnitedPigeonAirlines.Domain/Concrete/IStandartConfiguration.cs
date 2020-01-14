using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Data.Entities.EmailAggregate;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class IStandartConfiguration : IConfiguration
    {
        public string ChosenStrategyName { get; set; }
        public EmailSettings emailSettings { get; set; }
        public IStandartConfiguration()
        {
            ChosenStrategyName = "DefaultCalculationStrategy";
            emailSettings = new EmailSettings
            {
                MailToAddress = "egoppanchenko@gmail.com",
                MailFromAddress = "UnitedPigeonAirlines@example.com",
                UseSsl = true,
                Username = "MySmtpUsername",
                Password = "MySmtpPassword",
                ServerName = "smtp.example.com",
                ServerPort = 587,
                WriteAsFile = false,
                FileLocation = @"c:\EMAILS",
            };
        }
    }
}
