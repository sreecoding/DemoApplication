using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Asos.Identity.Core.Api.Authentication.Configuration;
using Autofac;
using Autofac.Integration.WebApi;
using DemoApplication.Services;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
using DemoApplication.ErrorHandler;
using DemoApplication.Repositories;
using Swashbuckle.Application;
//using Asos.Identity.Core.Api.Authentication.Configuration;

namespace DemoApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // SetupAzureActiveDirectoryAuthentication(config);
            // config.Filters.Add(new AuthorizeAttribute());

            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());


            BuildDependencyResolver();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index"));

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
           // );
        }

        private static void BuildDependencyResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GiftAidCalculatorFactory>().As<IGiftAidCalculatorFactory>();
            builder.RegisterType<GiftAidOrchestrationService>().As<IGiftAidOrchestrationService>();
            builder.RegisterType<RequestValidator>().As<IRequestValidator>();
            builder.RegisterType<GiftAidCalculator>().As<IGiftAidCalculator>();
            builder.RegisterType<HealthCheckService>().As<IHealthCheckService>();
            builder.RegisterType<HealthCheckFactory>().As<IHealthCheckFactory>();
            builder.RegisterType<HealthCheckResponseBuilder>().As<IHealthCheckResponseBuilder>();
            builder.RegisterType<DatabaseCheck>().As<IDatabaseCheck>();
            builder.RegisterType<TaxRepository>().As<ITaxRepository>();

            builder.RegisterType<GiftAidController>().UsingConstructor(typeof(IGiftAidOrchestrationService),typeof(IRequestValidator)).InstancePerRequest();

            builder.RegisterType<HealthCheckController>().UsingConstructor(typeof(IHealthCheckService)).InstancePerRequest();

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        //public static void SetupAzureActiveDirectoryAuthentication(HttpConfiguration config)
        //{
        //    var settings = new ApiSecuritySettingsFactory().Create();
        //    config.SecureApiUsingJwtBearerToken(settings);
        //}
    }

    
}
