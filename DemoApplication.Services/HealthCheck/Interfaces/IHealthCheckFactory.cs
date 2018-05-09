using System.Collections.Generic;

namespace DemoApplication.Services.HealthCheck
{
    public interface IHealthCheckFactory
    {
        IList<HealthCheckOutput> GenerateHealthCheckOutputs();
    }
}