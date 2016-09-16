using System.Web.Http;
using Owin;
using Microsoft.Practices.Unity;
using Order.API.Resolver;
using Order.DataLayer.Interfaces;
using Order.DataLayer;
using Order.BusinessLayer;
using Order.BusinessLayer.Interface;

namespace Order.API
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            UnityConfig.RegisterComponents(config);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IOrderManager, OrderManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDataLayerContext, DataLayerContext>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Other Web API configuration not shown.
        }
    }
}
