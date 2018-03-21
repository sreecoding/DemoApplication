using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.HealthCheck;
using DemoApplication.Infrastructure.HealthCheck;
using DemoApplication.Repositories;
using Moq;
using Shouldly;
using TechTalk.SpecFlow;

namespace DemoApplication.Specs
{
    [Binding]
    public class HealthCheckSteps
    {
        private Mock<IDatabaseCheck> _databaseCheck;
        private HealthCheckController _healthCheckController;
        private NegotiatedContentResult<HealthCheckResponse> _result;

        [Given(@"The Database Connection is up and running")]
        public void GivenTheDatabaseConnectionIsUpAndRunning()
        {
            _databaseCheck = new Mock<IDatabaseCheck>();

            _databaseCheck.Setup(x => x.CheckConnection()).Returns(true);
        }
        
        [When(@"I call the HealthCheckController")]
        public void WhenICallTheHealthCheckController()
        {
            _healthCheckController = new HealthCheckController(
                new HealthCheckService(new HealthCheckFactory(_databaseCheck.Object),new HealthCheckResponseBuilder()));

            _result = (NegotiatedContentResult<HealthCheckResponse>)_healthCheckController.Get();
        }
        
        [Then(@"the result should be that the system is up")]
        public void ThenTheResultShouldBeThatTheSystemIsUp()
        {
            _result.StatusCode.ShouldBe(HttpStatusCode.OK);
            _result.Content.IsSystemHealthy.ShouldBeTrue();
        }

        [Given(@"The Database Connection is down")]
        public void GivenTheDatabaseConnectionIsDown()
        {
            _databaseCheck = new Mock<IDatabaseCheck>();

            _databaseCheck.Setup(x => x.CheckConnection()).Returns(true);
        }

        [Then(@"the result should be that the system is down")]
        public void ThenTheResultShouldBeThatTheSystemIsDown()
        {
            _result.StatusCode.ShouldBe(HttpStatusCode.OK);
            _result.Content.IsSystemHealthy.ShouldBeTrue();
        }

    }
}
