using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DemoApplication.Services;
using DemoApplication.Controllers;

namespace DemoApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //AutoFac
            BuildDependencyResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void BuildDependencyResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GiftAidCalculatorFactory>().As<IGiftAidCalculatorFactory>();
            builder.RegisterType<GiftAidService>().As<IGiftAidService>();
            builder.RegisterType<GiftAidCalculator>().As<IGiftAidCalculator>();
            builder.RegisterType<TaxRepository>().As<ITaxRepository>();
            builder.RegisterType<GiftAidController>().UsingConstructor(typeof(IGiftAidService)).InstancePerRequest();
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }

    
}
