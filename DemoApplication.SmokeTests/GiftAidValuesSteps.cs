using System.Net;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Repositories;
using DemoApplication.Services;
using Newtonsoft.Json;
using RestSharp;
using Shouldly;
using TechTalk.SpecFlow;

namespace DemoApplication.SmokeTests
{
    [Binding]
    public class GiftAidValuesSteps
    {
        private int _donationAmount;
        private string _country;

        private GiftAidController _giftAidController;
        private OkNegotiatedContentResult<GiftAidResponse> _result;
        private IGiftAidOrchestrationService _giftAidOrchestrationService;
        private RestRequest _request;
        private IRestResponse _restResponse;

        [Given(@"I have a Donation Amount of 1000")]
        public void GivenIHaveADonationAmountOf()
        {
            _donationAmount = 1000;
        }
        
        [Given(@"the country where I make the donation is UK")]
        public void GivenTheCountryWhereIMakeTheDonationIsUk()
        {
            _country = "UK";
        }
        
        [When(@"a call is made to get Gift Aid")]
        public void WhenACallIsMadeToGetGiftAid()
        {
            _request = new RestRequest("/api/GiftAid/GetGiftAid", Method.GET);
            _request.Parameters.Add(new Parameter() {Name = "country",Value = _country,Type = ParameterType.QueryString});
            _request.Parameters.Add(new Parameter() {Name = "donationAmount",Value = _donationAmount,Type = ParameterType.QueryString});

            var client = new RestClient("http://localhost:63094");

            _restResponse = client.Execute(_request);
        }
        
        [Then(@"the response code from GiftAid endpoint should be OK")]
        public void ThenTheResponseCodeFromGiftAidEndpointShouldBeOk()
        {
            _restResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Then(@"the response contains the correct gift aid amount")]
        public void ThenTheResponseContainsTheCorrectGiftAidAmount()
        {
            var response = JsonConvert.DeserializeObject<GiftAidResponse>(_restResponse.Content);

            response.GiftAidAmount.ShouldBe(250);
        }
    }
}
