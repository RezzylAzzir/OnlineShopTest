using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedPigeonAirlines.Data.Entities.EmailAggregate
{
    public class EmailSettings
    {
        public string MailToAddress;
        public string MailFromAddress;
        public bool UseSsl;
        public string Username;
        public string Password;
        public string ServerName;
        public int ServerPort;
        public bool WriteAsFile;
        public string FileLocation;
    }
}
