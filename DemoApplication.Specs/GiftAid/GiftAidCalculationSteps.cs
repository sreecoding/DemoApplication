using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.GiftAid;
using DemoApplication.Infrastructure.GiftAid;
using DemoApplication.Repositories;
using DemoApplication.Services;
using Moq;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoApplication.Specs.GiftAid
{
    [Binding]
    public class GiftAidCalculationSteps
    {
        private GiftAidController _giftAidController;
        private NegotiatedContentResult<GiftAidResponse> _result;
        private Mock<ITaxRepository> _taxRepository;
        private IGiftAidOrchestrationService _giftAidOrchestrationService;
        private decimal _donation;
        private string _country;
        private string _event;
        private List<IGiftAidCalculator> _giftAidCalculators;


        [Given(@"We have the Following Tax Data in the database")]
        public void GivenWeHaveTheFollowingTaxDataInTheDatabase(Table table)
        {
            var taxData = table.CreateSet<TaxData>();
            _taxRepository = new Mock<ITaxRepository>();
            _taxRepository.Setup(x => x.GetTaxRate("UK")).Returns(Task.FromResult(taxData.ToList()));
        }

        [Given(@"the Donation Amount is (.*) pounds")]
        public void GivenIHavePaidPoundsAsDonation(int p0)
        {
            _donation = p0;
        }

        [Given(@"the Donation Country is (.*)")]
        public void GivenTheDonationCountryIsUk(string country)
        {
            _country = country;
        }

        [Given(@"The Event is (.*)")]
        public void GivenTheEventIsSwimming(string giftEvent)
        {
            _event = giftEvent;
        }

        [When(@"I make the Donation")]
        public async void WhenIMakeTheDonation()
        {
            _giftAidCalculators = new List<IGiftAidCalculator>(){ new GeneralGiftAidCalculator(), new SwimmingGiftAidCalculator()};

            _giftAidOrchestrationService =new GiftAidOrchestrationService(_taxRepository.Object, new GiftAidCalculatorFinder(_giftAidCalculators));

            _giftAidController = new GiftAidController(_giftAidOrchestrationService, new RequestValidator(_giftAidCalculators));

            _result = (NegotiatedContentResult<GiftAidResponse>)await _giftAidController.GetGiftAid(_donation, _country, _event);
        }

        [Then(@"the Total Gift Aid Amount Should be (.*) pounds")]
        public void ThenTheTotalGiftAidAmountShouldBePounds(int giftAidAmount)
        {
            _result.Content.GiftAidAmount.ShouldBe(giftAidAmount);
        }

        [Then(@"the Status Code should be Ok")]
        public void ThenTheStatusCodeShouldBeOk()
        {
            _result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Then(@"we get a BadRequest Response")]
        public void ThenWeGetABadRequestResponse()
        {
            _result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

    }
}
