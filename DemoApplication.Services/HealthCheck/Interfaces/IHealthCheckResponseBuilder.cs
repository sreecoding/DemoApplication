using System.Collections.Generic;

namespace DemoApplication.Services.HealthCheck
{
    public interface IHealthCheckResponseBuilder
    {
        HealthCheckResponse GenerateHealthCheckResponse(IList<HealthCheckOutput> healthCheckOutputs);
    }
}