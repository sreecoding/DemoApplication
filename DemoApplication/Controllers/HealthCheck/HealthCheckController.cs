using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DemoApplication.Infrastructure.HealthCheck;

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

        /// <summary>
        /// Get health of the system
        /// </summary>
        /// <remarks>
        /// Returns the health of the system and sub systems
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("HealthCheck/Get")]
        [ResponseType(typeof(HealthCheckResponse))]
        public IHttpActionResult Get()
        {
            var healthCheckResponse = _healthCheckService.CheckSystemHealth();

            if (healthCheckResponse.IsSystemHealthy)
                return Content(HttpStatusCode.OK,healthCheckResponse);

            return Content(HttpStatusCode.InternalServerError, healthCheckResponse);
        }
    }
 
}
