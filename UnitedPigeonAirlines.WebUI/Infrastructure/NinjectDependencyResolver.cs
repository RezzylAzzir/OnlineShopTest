using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.WebUI.Infrastructure.Abstract;
using UnitedPigeonAirlines.WebUI.Infrastructure.Concrete;
using System.Configuration;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.EF.Repositories;

namespace UnitedPigeonAirlines.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            
            kernel.Bind<IPigeonRepository>().To<EFPigeonRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}