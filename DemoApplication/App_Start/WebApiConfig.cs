using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DemoApplication.Services;
using DemoApplication.Controllers;
using DemoApplication.Repositories;
using Swashbuckle.Application;

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
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index"));

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
            builder.RegisterType<GiftAidOrchestrationService>().As<IGiftAidOrchestrationService>();
            builder.RegisterType<RequestValidator>().As<IRequestValidator>();
            builder.RegisterType<GiftAidCalculator>().As<IGiftAidCalculator>();
            builder.RegisterType<TaxRepository>().As<ITaxRepository>();
            builder.RegisterType<GiftAidController>().UsingConstructor(typeof(IGiftAidOrchestrationService),typeof(IRequestValidator)).InstancePerRequest();
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }

    
}
