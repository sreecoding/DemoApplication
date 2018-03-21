using DemoApplication.Controllers.HealthCheck;

namespace DemoApplication.Controllers
{
    public interface IHealthCheckService
    {
        HealthCheckResponse CheckSystemHealth();
    }


    public class HealthCheckService : IHealthCheckService
    {
        private IHealthCheckFactory healthCheckFactory;
        private IHealthCheckResponseBuilder healthCheckResponseBuilder;

        public HealthCheckService(IHealthCheckFactory healthCheckFactory, IHealthCheckResponseBuilder healthCheckResponseBuilder)
        {
            this.healthCheckFactory = healthCheckFactory;
            this.healthCheckResponseBuilder = healthCheckResponseBuilder;
        }

        public HealthCheckResponse CheckSystemHealth()
        {
            var healthCheckOutputs = healthCheckFactory.GenerateHealthCheckOutputs();

            return healthCheckResponseBuilder.GenerateHealthCheckResponse(healthCheckOutputs);
        }
    }
}