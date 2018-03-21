using System.Collections.Generic;
using System.Linq;
using DemoApplication.Infrastructure.HealthCheck;

namespace DemoApplication.Controllers
{

    public interface IHealthCheckResponseBuilder
    {
        HealthCheckResponse GenerateHealthCheckResponse(IList<HealthCheckOutput> healthCheckOutputs);
    }


    public class HealthCheckResponseBuilder : IHealthCheckResponseBuilder
    {
        public HealthCheckResponse GenerateHealthCheckResponse(IList<HealthCheckOutput> healthCheckOutputs)
        {
            return new HealthCheckResponse()
            {
                IsSystemHealthy = healthCheckOutputs.All(h => h.IsHealthy),
                SubSystemHealthCheckOutputs = healthCheckOutputs
            };
        }
    }
}