﻿using System.Collections.Generic;
using System.Linq;
using DemoApplication.Controllers;
using DemoApplication.Domain;
using DemoApplication.Infrastructure.HealthCheck;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services.HealthCheck
{
    [TestFixture]
    public class HealthCheckServiceShould
    {
        private IHealthCheckService _healthCheckService;
        private Mock<IHealthCheckFactory> _mockHealthCheckfactory;
        private Mock<IHealthCheckResponseBuilder> _mockHealthCheckResponseBuilder;
        private HealthCheckResponse _subSystemHealthCheckResponse;
        private List<HealthCheckOutput> _subSystemHealthCheckOutputs;

        [SetUp]
        public void Setup()
        {
            _mockHealthCheckfactory = new Mock<IHealthCheckFactory>();
            _mockHealthCheckResponseBuilder = new Mock<IHealthCheckResponseBuilder>();

            _healthCheckService = new HealthCheckService(_mockHealthCheckfactory.Object,
                                                    _mockHealthCheckResponseBuilder.Object);
            _subSystemHealthCheckOutputs = new List<HealthCheckOutput>
                { new HealthCheckOutput { DependencyName = HealthCheckConstants.SubSystem.SqlDatabase, IsHealthy = true } };
        }

     
        [TestCase(true)]
        [TestCase(false)]
        public void GivenDatabaseHealthList_ReturnsHealthResponse(bool isSystemHealthy)
        {
            var healthCheckOutputs = ConfigureHealthCheckOutputs(isSystemHealthy);

            ConfigureHealthCheckResponse(isSystemHealthy, healthCheckOutputs);

            var response = _healthCheckService.CheckSystemHealth();

            response.IsSystemHealthy.ShouldBe(isSystemHealthy);
        }

        
        [TestCase(true)]
        [TestCase(false)]
        public void GivenSubSystemHealth_ReturnsCorrectResponse(bool isSystemHealthy)
        {
            var healthCheckOutputs = ConfigureHealthCheckOutputs(isSystemHealthy);

            ConfigureHealthCheckResponse(isSystemHealthy, healthCheckOutputs);

            var response = _healthCheckService.CheckSystemHealth();

            var subSystemOutput = response.SubSystemHealthCheckOutputs.Single();

            subSystemOutput.DependencyName.ShouldBe(HealthCheckConstants.SubSystem.SqlDatabase);
            subSystemOutput.IsHealthy.ShouldBe(true);
        }

        private void ConfigureHealthCheckResponse(bool isSystemHealthy, List<HealthCheckOutput> healthCheckOutputs)
        {
            _subSystemHealthCheckResponse = new HealthCheckResponse()
            {
                IsSystemHealthy = isSystemHealthy,
                SubSystemHealthCheckOutputs =_subSystemHealthCheckOutputs
            };

            _mockHealthCheckResponseBuilder.Setup(x => x.GenerateHealthCheckResponse(healthCheckOutputs))
                .Returns(_subSystemHealthCheckResponse);
        }

        private List<HealthCheckOutput> ConfigureHealthCheckOutputs(bool isSystemHealthy)
        {
            var healthCheckOutputs = new List<HealthCheckOutput>()
            {
                new HealthCheckOutput() {DependencyName =  HealthCheckConstants.SubSystem.SqlDatabase, IsHealthy = isSystemHealthy}
            };

            _mockHealthCheckfactory.Setup(x => x.GenerateHealthCheckOutputs())
                .Returns(healthCheckOutputs);

            return healthCheckOutputs;
        }
    }
}
