using System.Collections.Generic;
using DemoApplication.Controllers;
using DemoApplication.Domain;
using DemoApplication.Infrastructure.HealthCheck;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services.HealthCheck
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

        [Test]
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
