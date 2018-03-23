using System.Collections.Generic;
using DemoApplication.Infrastructure;
using DemoApplication.Infrastructure.GiftAid;
using DemoApplication.Repositories;
using DemoApplication.Services;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services.GiftAid
{
    [TestFixture]
    public class GiftAidOrchestrationServiceShould
    {
        private IGiftAidOrchestrationService _giftAidOrchestrationService;
        private Mock<ITaxRepository> _taxRepository;
        private Mock<IGiftAidCalculatorFinder> _giftAidCalculatorFinder;
        private Mock<IGiftAidCalculator> _mockGiftAidCalculator;
        private TaxData _taxData;
        private List<TaxData> _taxList;

        [SetUp]
        public void Setup()
        {
            _giftAidCalculatorFinder = new Mock<IGiftAidCalculatorFinder>();
            _taxRepository = new Mock<ITaxRepository>();
            _mockGiftAidCalculator = new Mock<IGiftAidCalculator>();
            _giftAidOrchestrationService = new GiftAidOrchestrationService(_taxRepository.Object,_giftAidCalculatorFinder.Object);

            _taxData = new TaxData { Country = "UK", TaxRate = 20 };
            _taxList = new List<TaxData> { _taxData };

            _taxRepository.Setup(x => x.GetTaxRate("UK"))
                .Returns(_taxList);
        }

        [Test]
        [TestCase("General","UK",25)]
        [TestCase("Swimming", "UK", 30)]
        public void GivenGeneralDonationandCountry_ReturnsGiftAid(string eventType, string country, int giftAidValue)
        {
            _giftAidCalculatorFinder.Setup(x => x.Find(eventType)).Returns(_mockGiftAidCalculator.Object);

            _mockGiftAidCalculator.Setup(x => x.Calculate(100, country, _taxData)).Returns(giftAidValue);

            var giftAid = _giftAidOrchestrationService.CalculateGiftAid(100, country,eventType);

            giftAid.ShouldBe(giftAidValue);
        }

        [Test]
        [TestCase("General")]
        [TestCase("Swimming")]
        public void GivenNoTaxRateForCountry_ReturnsGiftAidAsZero(string eventType)
        {
            _taxRepository.Setup(x => x.GetTaxRate("US"))
                .Returns(new List<TaxData>());

            var giftAid = _giftAidOrchestrationService.CalculateGiftAid(100, "US", eventType);

            giftAid.ShouldBe(0);
        }
    }
}
