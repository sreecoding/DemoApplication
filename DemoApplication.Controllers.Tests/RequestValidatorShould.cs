using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApplication.Domain;
using DemoApplication.Repositories;
using DemoApplication.Services.GiftAid;
using DemoApplication.Services.GiftAid.Interfaces;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Controllers.Tests
{
    [TestFixture]
    public class RequestValidatorShould
    {
        private List<IGiftAidCalculator> _giftAidCalculators;
        private Mock<ICountryService> _countryService;
        private IRequestValidator _requestValidator;

        [SetUp]
        public void Setup()
        {
            _countryService = new Mock<ICountryService>();

            _giftAidCalculators = new List<IGiftAidCalculator>
            {
                new GeneralGiftAidCalculator(),
                new SwimmingGiftAidCalculator()
            };

            _requestValidator = new RequestValidator(_giftAidCalculators, _countryService.Object);
        }

        [Test]
        public async Task GivenValidInput_ReturnsNoError()
        {
            var countries = new List<Country>() { new Country("TS", "Test") };

            _countryService.Setup(x => x.GetCountryByCountryCode("TS"))
                .Returns(Task.FromResult(countries));

            var validationError = await _requestValidator.Validate(100, "TS", "Swimming");

            validationError.Count.ShouldBe(0);
        }


        [Test]
        public async Task GivenInvalidCountry_ReturnsError()
        {
            _countryService.Setup(x => x.GetCountryByCountryCode("TS"))
                .Returns(Task.FromResult((List<Country>)null));

            var validationError = await _requestValidator.Validate(100, "TS", "Swimming");
            
            validationError.Single().ParameterName.ShouldBe(GiftAidConstants.InputFields.Country);
        }

        [Test]
        public async Task GivenEmptyCountry_ReturnsError()
        {
            var validationError = await _requestValidator.Validate(100, "", "Swimming");

            validationError.Single().ParameterName.ShouldBe(GiftAidConstants.InputFields.Country);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GivenInvalidDonation_ReturnsError(int donationAmount)
        {
            var countries = new List<Country>() {new Country("TS","Test")};

            _countryService.Setup(x => x.GetCountryByCountryCode("TS"))
                .Returns(Task.FromResult(countries));

            var validationError = await _requestValidator.Validate(donationAmount, "TS", "Swimming");

            validationError.Single().ParameterName.ShouldBe(GiftAidConstants.InputFields.Donation);
        }

        [Test]
        public async Task GivenInvalidType_ReturnsError()
        {
            var countries = new List<Country>() { new Country("TS", "Test") };

            _countryService.Setup(x => x.GetCountryByCountryCode("TS"))
                .Returns(Task.FromResult(countries));

            var validationError = await _requestValidator.Validate(100, "TS", "Invalid");

            validationError.Single().ParameterName.ShouldBe(GiftAidConstants.InputFields.EventType);
        }




    }
}
