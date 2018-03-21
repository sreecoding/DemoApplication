using System;
using System.Collections.Generic;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
using DemoApplication.Infrastructure.HealthCheck;
using DemoApplication.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;

namespace DemoApplication.Tests.Services
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

        [Test]
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
