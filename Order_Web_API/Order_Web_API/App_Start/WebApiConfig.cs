using Order_Web_API.Helper;
using Order_Web_API.IServices;
using Order_Web_API.Services;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Order_Web_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();

            var container = new UnityContainer();          
            container.RegisterType<IOrderService, OrderService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger/ui/index/swagger/ui/index"));
        }
    }
}
