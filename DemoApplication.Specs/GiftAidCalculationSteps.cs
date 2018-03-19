using System;
using System.Net;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Services;
using Moq;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoApplication.Specs
{
    [Binding]
    public class GiftAidCalculationSteps
    {
        private GiftAidController _giftAidController;
        private NegotiatedContentResult<decimal> _result;
        private Mock<ITaxRepository> _taxRepository;
        private IGiftAidOrchestrationService _giftAidOrchestrationService;

        [Given(@"I have paid (.*) pounds as Donation")]
        public void GivenIHavePaidPoundsAsDonation(int p0)
        {
        }
        
        [Given(@"I have  No Gift Aid Excemption")]
        public void GivenIHaveNoGiftAidExcemption()
        {
        }
        
        [Then(@"the Total Gift Amount Should be (.*) pounds")]
        public void ThenTheGiftAidAmountShouldBePounds(Decimal p0)
        {
            _result.StatusCode.ShouldBe(HttpStatusCode.OK);
            _result.Content.ShouldBe(p0);
        }

        [Given(@"We have the Following Tax Data in the database")]
        public void GivenWeHaveTheFollowingTaxDataInTheDatabase(Table table)
        {
            var taxData = table.CreateSet<TaxData>();
            _taxRepository = new Mock<ITaxRepository>();
            _taxRepository.Setup(x => x.GetTaxRate("UK")).Returns(taxData);
        }

        [When(@"I make the Donation of (.*) in (.*)")]
        public void WhenIMakeTheDonationInUk(decimal donationAmount, string country)
        {
            _giftAidOrchestrationService = new GiftAidOrchestrationService(new GiftAidCalculator(_taxRepository.Object));

            _giftAidController = new GiftAidController(_giftAidOrchestrationService);

            _result = (NegotiatedContentResult<decimal>) _giftAidController.GetGiftAid(donationAmount, country);
        }

    }
}
