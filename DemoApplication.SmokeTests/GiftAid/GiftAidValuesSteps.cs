using System.Net;
using DemoApplication.Controllers;
using Newtonsoft.Json;
using RestSharp;
using Shouldly;
using TechTalk.SpecFlow;

namespace DemoApplication.SmokeTests.GiftAid
{
    [Binding]
    public class GiftAidValuesSteps
    {
        private const string GiftaidApiEndpoint = "/api/GiftAid/GetGiftAid";
        private const string BaseUrl = "http://localhost:63094";
        private int _donationAmount;
        private string _country;
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
            _request = new RestRequest(GiftaidApiEndpoint, Method.GET);
            _request.Parameters.Add(new Parameter() {Name = "country",Value = _country,Type = ParameterType.QueryString});
            _request.Parameters.Add(new Parameter() {Name = "donationAmount",Value = _donationAmount,Type = ParameterType.QueryString});

            var client = new RestClient(BaseUrl);

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
