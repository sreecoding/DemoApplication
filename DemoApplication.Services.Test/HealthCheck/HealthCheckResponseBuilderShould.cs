using System.Collections.Generic;
using DemoApplication.Domain;
using DemoApplication.Services.HealthCheck;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Services.Test.HealthCheck
{
    [TestFixture]
    public class HealthCheckResponseBuilderShould
    {
        private IHealthCheckResponseBuilder _healthCheckResponseBuilder;
        private HealthCheckResponse _healthCheckResponse;


        [SetUp]
        public void Setup()
        {
            _healthCheckResponseBuilder = new HealthCheckResponseBuilder();
        }

       
        [TestCase(true)]
        public void GivenHealthChecks_BuildsCorrectOutput(bool isHealthy)
        {
            var healthCheckOutputs = new List<HealthCheckOutput>()
            {
                new HealthCheckOutput() {DependencyName = HealthCheckConstants.SubSystem.SqlDatabase, IsHealthy = isHealthy}
            };

            _healthCheckResponse = _healthCheckResponseBuilder.GenerateHealthCheckResponse(healthCheckOutputs);

            _healthCheckResponse.IsSystemHealthy.ShouldBe(isHealthy);

        }
    }
}
