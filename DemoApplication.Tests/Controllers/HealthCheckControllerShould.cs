using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
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
        public void GivenHealthySystem_ReturnStatusOK()
        {
            _mockHealthCheckService.Setup(x => x.CheckSystemHealth())
                .Returns(new HealthCheckResponse(){IsSystemHealthy = true});

            _result = (NegotiatedContentResult<HealthCheckResponse>)_healthCheckController.Get();

            _result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public void GivenUnHealthySystem_ReturnStatusError()
        {
            _mockHealthCheckService.Setup(x => x.CheckSystemHealth())
                .Returns(new HealthCheckResponse() { IsSystemHealthy = false });

            _result = (NegotiatedContentResult<HealthCheckResponse>)_healthCheckController.Get();

            _result.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        }

    }
}
