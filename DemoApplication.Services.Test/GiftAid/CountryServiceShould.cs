using System.Threading.Tasks;
using DemoApplication.Repositories;
using DemoApplication.Repositories.Interfaces;
using DemoApplication.Services.GiftAid;
using DemoApplication.Services.GiftAid.Interfaces;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

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
        public async Task GivenCountryCode_ReturnsCountry()
        {
            var countryExpected = new Country("US", "United States");

            var countryListExpected = new List<Country> {countryExpected};

            _countryRepository
                .Setup(x => x.GetCountryByCountryCode(CountryCode))
                .Returns(Task.FromResult(countryListExpected));

            var countryList = await _countryService.GetCountryByCountryCode(CountryCode);

            countryList.ShouldBeSameAs(countryListExpected);

        }
    }
}
