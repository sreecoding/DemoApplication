using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using DemoApplication.Controllers;
using DemoApplication.Controllers.GiftAid;
using DemoApplication.Controllers.HealthCheck;
using DemoApplication.ErrorHandler;
using DemoApplication.Services.GiftAid;
using DemoApplication.Services.HealthCheck;
using DemoApplication.Repositories;
using DemoApplication.Services;
using Swashbuckle.Application;


namespace DemoApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

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
            
        }

        private static void BuildDependencyResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GiftAidCalculatorFinder>().As<IGiftAidCalculatorFinder>();
            builder.RegisterType<GiftAidOrchestrationService>().As<IGiftAidOrchestrationService>();
            builder.RegisterType<RequestValidator>().As<IRequestValidator>();
            builder.RegisterType<HealthCheckService>().As<IHealthCheckService>();
            builder.RegisterType<HealthCheckFactory>().As<IHealthCheckFactory>();
            builder.RegisterType<HealthCheckResponseBuilder>().As<IHealthCheckResponseBuilder>();
            builder.RegisterType<DatabaseCheck>().As<IDatabaseCheck>();
            builder.RegisterType<TaxRepository>().As<ITaxRepository>();

            builder.RegisterType<GeneralGiftAidCalculator>().As<IGiftAidCalculator>();
            builder.RegisterType<SwimmingGiftAidCalculator>().As<IGiftAidCalculator>();

            builder.RegisterType<GiftAidController>().UsingConstructor(typeof(IGiftAidOrchestrationService),typeof(IRequestValidator)).InstancePerRequest();

            builder.RegisterType<HealthCheckController>().UsingConstructor(typeof(IHealthCheckService)).InstancePerRequest();

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }

    
}
