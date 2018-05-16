using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using TechTalk.SpecFlow;

namespace DemoApplication.SmokeTests.HealthCheck
{
    [Binding]
    public class HealthCheckSteps
    {
        private readonly string _healthcheckEndpoint = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] 
                                                      + System.Configuration.ConfigurationManager.AppSettings["HealthcheckEndpoint"];

        private HttpResponseMessage _httpResponseMessage;

       
        [When(@"I call the HealthCheck EndPoint")]
        public async Task WhenICallTheHealthCheckEndPoint()
        {
           using (var client = new HttpClient())
           {
                _httpResponseMessage = await client.GetAsync(_healthcheckEndpoint);
           }
        }

        [Then(@"the result should be that the system is up")]
        public void ThenTheResultShouldBeThatTheSystemIsUp()
        {
            _httpResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
    }
}
