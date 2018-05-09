namespace DemoApplication.Services.HealthCheck
{
    public interface IHealthCheckService
    {
        HealthCheckResponse CheckSystemHealth();
    }
}