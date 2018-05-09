using System.Collections.Generic;

namespace DemoApplication.Services.HealthCheck
{
    public class HealthCheckResponse
    {
        public bool IsSystemHealthy { get; set; }

        public IList<HealthCheckOutput> SubSystemHealthCheckOutputs { get; set; }

    }
}
