using System.Collections.Generic;
using System.Linq;

namespace DemoApplication.Services.HealthCheck
{
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