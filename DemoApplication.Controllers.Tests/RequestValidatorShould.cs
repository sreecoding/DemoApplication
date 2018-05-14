using System.Collections.Generic;
using System.Linq;
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
        public void GivenInvalidCountry_ReturnsError()
        {
            _countryService.Setup(x => x.GetCountryByCountryCode("TS"))
                .Returns((Country) null);

            var validationError = _requestValidator.Validate(100, "TS", "Swimming").Single();

            //validationError.ErrorMessage.ShouldBe(testErrorMessage);
            validationError.ParameterName.ShouldBe(GiftAidConstants.InputFields.Country);
        }
    }
}
