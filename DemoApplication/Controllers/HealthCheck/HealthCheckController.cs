using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace DemoApplication.Controllers.HealthCheck
{
    [RoutePrefix("api")]
    public class HealthCheckController : ApiController
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        [Route("HealthCheck/Get")]
        public IHttpActionResult Get()
        {
            var healthCheckResponse = _healthCheckService.CheckSystemHealth();

            if (healthCheckResponse.IsSystemHealthy)
                return Content(HttpStatusCode.OK,healthCheckResponse);

            return Content(HttpStatusCode.InternalServerError, healthCheckResponse);
        }
    }

    public class HealthCheckResponse
    {
        public bool IsSystemHealthy { get; set; }

        public IList<HealthCheckOutput> SubSystemHealthCheckOutputs { get; set; }
         
    }

    
    public class HealthCheckOutput
    {
        public bool IsHealthy { get; set; }

        public string DependencyName { get; set; }
    }

   

 
}
