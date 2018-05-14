using System;
using DemoApplication.Repositories;
using DemoApplication.Repositories.Interfaces;
using DemoApplication.Services.GiftAid;
using DemoApplication.Services.GiftAid.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Services.Test.GiftAid
{
    [TestFixture]
    public class CountryServiceShould
    {
        private const string CountryCode = "US";
        private Mock<ICountryRepository> _countryRepository;
        private ICountryService _countryService;

        [SetUp]
        public void Setup()
        {
            _countryRepository = new Mock<ICountryRepository>();
            _countryService = new CountryService(_countryRepository.Object);
        }

        [Test]
        public void GivenCountryCode_ReturnsCountry()
        {
            var countryExpected = new Country("US", "United States");

            _countryRepository
                .Setup(x => x.GetCountryByCountryCode(CountryCode))
                .Returns(countryExpected);

            var country = _countryService.GetCountryByCountryCode(CountryCode);

            country.ShouldBeSameAs(countryExpected);

        }
    }
}
