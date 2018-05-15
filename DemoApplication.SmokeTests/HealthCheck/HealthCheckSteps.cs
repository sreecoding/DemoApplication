using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
using Shouldly;
using TechTalk.SpecFlow;
using HttpResponse = System.Web.HttpResponse;

namespace DemoApplication.SmokeTests.HealthCheck
{
    [Binding]
    public class HealthCheckSteps
    {
        private const string HealthcheckEndpoint = "http://localhost:63094/api/HealthCheck/Get";

        private HttpRequest _request;
        private HttpResponseMessage _httpResponseMessage;

       
        [When(@"I call the HealthCheck EndPoint")]
        public async Task WhenICallTheHealthCheckEndPoint()
        {
           using (var client = new HttpClient())
           {
                _httpResponseMessage = await client.GetAsync(HealthcheckEndpoint);
           }
        }

        [Then(@"the result should be that the system is up")]
        public void ThenTheResultShouldBeThatTheSystemIsUp()
        {
            _httpResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
    }
}
