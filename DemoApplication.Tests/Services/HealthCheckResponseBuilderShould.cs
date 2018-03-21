using System;
using System.Collections.Generic;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
using DemoApplication.Infrastructure.HealthCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;

namespace DemoApplication.Tests.Services
{
    [TestFixture]
    public class HealthCheckResponseBuilderShould
    {
        private IHealthCheckResponseBuilder healthCheckResponseBuilder;
        private HealthCheckResponse _healthCheckResponse;


        [SetUp]
        public void Setup()
        {
            healthCheckResponseBuilder = new HealthCheckResponseBuilder();
        }

        [Test]
        [TestCase(true)]
        public void GivenHealthChecks_BuildsCorrectOutput(bool isHealthy)
        {
            var healthCheckOutputs = new List<HealthCheckOutput>()
            {
                new HealthCheckOutput() {DependencyName = "SQL Database", IsHealthy = isHealthy}
            };

            _healthCheckResponse = healthCheckResponseBuilder.GenerateHealthCheckResponse(healthCheckOutputs);

            _healthCheckResponse.IsSystemHealthy.ShouldBe(isHealthy);

        }
    }
}
