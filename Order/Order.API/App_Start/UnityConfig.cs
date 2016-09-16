using Order.API.Resolver;
using Order.BusinessLayer;
using Order.BusinessLayer.Interface;
using Order.DataLayer;
using Order.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Order.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IOrderManager, OrderManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();

            // config.DependencyResolver = new UnityDependencyResolver(container);
            config.DependencyResolver = new UnityResolver(container);


        }
    }
}