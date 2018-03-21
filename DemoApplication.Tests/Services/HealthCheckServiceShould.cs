using System;
using System.Collections.Generic;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services
{
    [TestFixture]
    public class HealthCheckServiceShould
    {
        private IHealthCheckService _healthCheckService;
        private Mock<IHealthCheckFactory> _mockHealthCheckfactory;
        private Mock<IHealthCheckResponseBuilder> _mockHealthCheckResponseBuilder;

        [SetUp]
        public void Setup()
        {
            _mockHealthCheckfactory = new Mock<IHealthCheckFactory>();
            _mockHealthCheckResponseBuilder = new Mock<IHealthCheckResponseBuilder>();

            _healthCheckService = new HealthCheckService(_mockHealthCheckfactory.Object,
                                                    _mockHealthCheckResponseBuilder.Object);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void GivenDatabaseHealthList_ReturnsHealthResponse(bool isSystemHealthy)
        {
            var healthCheckOutputs = new List<HealthCheckOutput>()
            {
                new HealthCheckOutput() {DependencyName = "SQL Database", IsHealthy = isSystemHealthy}
            };
            _mockHealthCheckfactory.Setup(x => x.GenerateHealthCheckOutputs())
                .Returns(healthCheckOutputs);

            _mockHealthCheckResponseBuilder.Setup(x => x.GenerateHealthCheckResponse(healthCheckOutputs))
                .Returns(new HealthCheckResponse() {IsSystemHealthy = isSystemHealthy});

            var response = _healthCheckService.CheckSystemHealth();

            response.IsSystemHealthy.ShouldBe(isSystemHealthy);
        }
    }
}
