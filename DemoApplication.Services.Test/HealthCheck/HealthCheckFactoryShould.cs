﻿using System.Collections.Generic;
using DemoApplication.Services.HealthCheck;
using DemoApplication.Repositories;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Services.Test.HealthCheck
{
    [TestFixture]
    public class HealthCheckFactoryShould
    {
        private Mock<IDatabaseCheck> _mockDataBaseCheck;
        private HealthCheckFactory _healthCheckFactory;
        private IList<HealthCheckOutput> _healthCheckOutputs;

        [SetUp]
        public void Setup()
        {
            _mockDataBaseCheck = new Mock<IDatabaseCheck>();
            _healthCheckFactory = new HealthCheckFactory(_mockDataBaseCheck.Object);

        }

       
        [TestCase(true)]
        [TestCase(false)]
        public void ReturnCorrectResponse(bool healthStatus)
        {
            _mockDataBaseCheck.Setup(x => x.CheckConnection())
                .Returns(healthStatus);

            _healthCheckOutputs = _healthCheckFactory.GenerateHealthCheckOutputs();

            _healthCheckOutputs[0].IsHealthy.ShouldBe(healthStatus);
        }
    }
}
