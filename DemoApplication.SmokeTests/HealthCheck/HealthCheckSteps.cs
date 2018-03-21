using System.Net;
using RestSharp;
using Shouldly;
using TechTalk.SpecFlow;

namespace DemoApplication.SmokeTests.HealthCheck
{
    [Binding]
    public class HealthCheckSteps
    {
        private const string HealthcheckEndpoint = "/api/HealthCheck/Get";
        private const string BaseUrl = "http://localhost:63094";
        //private  readonly string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];

        private RestRequest _request;
        private IRestResponse _restResponse;

       
        [When(@"I call the HealthCheck EndPoint")]
        public void WhenICallTheHealthCheckEndPoint()
        {
            _request = new RestRequest(HealthcheckEndpoint, Method.GET);
            var client = new RestClient(BaseUrl);
            _restResponse = client.Execute(_request);

        }

        [Then(@"the result should be that the system is up")]
        public void ThenTheResultShouldBeThatTheSystemIsUp()
        {
            _restResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            
        }
        
    }
}
