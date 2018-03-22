﻿using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
using DemoApplication.Infrastructure.HealthCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Controllers
{
    [TestFixture]
    public class HealthCheckControllerShould
    {
        private HealthCheckController _healthCheckController;
        private Mock<IHealthCheckService> _mockHealthCheckService;
        private NegotiatedContentResult<HealthCheckResponse> _result;

        [SetUp]
        public void Setup()
        {
            _mockHealthCheckService = new Mock<IHealthCheckService>();
            _healthCheckController = new HealthCheckController(_mockHealthCheckService.Object);
        }

        [Test]
        [TestCase(true,ExpectedResult = HttpStatusCode.OK)]
        [TestCase(false,ExpectedResult = HttpStatusCode.InternalServerError)]
        public HttpStatusCode GivenHealthySystem_ReturnStatusOK(bool isSystemHealthy)
        {
            _mockHealthCheckService.Setup(x => x.CheckSystemHealth())
                .Returns(new HealthCheckResponse(){IsSystemHealthy = isSystemHealthy});

            _result = (NegotiatedContentResult<HealthCheckResponse>)_healthCheckController.Get();

           return  _result.StatusCode;
        }
    }
}
