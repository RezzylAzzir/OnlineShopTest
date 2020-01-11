using System.Net;
using System.Net.Mail;
using System.Text;

namespace UnitedPigeonAirlines.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "egoppanchenko@gmail.com";
        public string MailFromAddress = "UnitedPigeonAirlines@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\EMAILS";
    }
}