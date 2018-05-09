namespace DemoApplication.Services.HealthCheck
{
    public class HealthCheckOutput
    {
        public bool IsHealthy { get; set; }

        public string DependencyName { get; set; }
    }
}
