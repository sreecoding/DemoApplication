using DemoApplication.Controllers;

namespace DemoApplication.Infrastructure.HealthCheck
{
    public interface IHealthCheckService
    {
        HealthCheckResponse CheckSystemHealth();
    }

    public class HealthCheckService : IHealthCheckService
    {
        private readonly IHealthCheckFactory _healthCheckFactory;
        private readonly IHealthCheckResponseBuilder _healthCheckResponseBuilder;

        public HealthCheckService(IHealthCheckFactory healthCheckFactory, IHealthCheckResponseBuilder healthCheckResponseBuilder)
        {
            this._healthCheckFactory = healthCheckFactory;
            this._healthCheckResponseBuilder = healthCheckResponseBuilder;
        }

        public HealthCheckResponse CheckSystemHealth()
        {
            var healthCheckOutputs = _healthCheckFactory.GenerateHealthCheckOutputs();

            return _healthCheckResponseBuilder.GenerateHealthCheckResponse(healthCheckOutputs);
        }
    }
}