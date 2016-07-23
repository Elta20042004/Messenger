using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Logging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Server
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            InitUnity();

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

        private static void InitUnity()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IMessageStore, MessageStore>(new ContainerControlledLifetimeManager())
                .RegisterType<IContactsMapping, ContactsMapping>(new ContainerControlledLifetimeManager())
                .RegisterType<LoginPasswordVerification, LoginPasswordVerification>(new ContainerControlledLifetimeManager());

            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
