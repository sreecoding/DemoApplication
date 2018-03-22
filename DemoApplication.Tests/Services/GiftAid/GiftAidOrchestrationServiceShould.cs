using System.Collections.Generic;
using DemoApplication.Infrastructure;
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

        [SetUp]
        public void Setup()
        {
            _giftAidCalculatorFinder = new Mock<IGiftAidCalculatorFinder>();
            _taxRepository = new Mock<ITaxRepository>();
            _mockGiftAidCalculator = new Mock<IGiftAidCalculator>();
            _giftAidOrchestrationService = new GiftAidOrchestrationService(_taxRepository.Object,_giftAidCalculatorFinder.Object);
        }

        [Test]
        public void GivenGeneralDonationandCountry_ReturnsGiftAid()
        {
            var taxData = new TaxData {Country = "UK", TaxRate = 20};
            var taxList = new List<TaxData> {taxData};
            
            _taxRepository.Setup(x => x.GetTaxRate("UK"))
                .Returns(taxList);

            _giftAidCalculatorFinder.Setup(x => x.Find("General")).Returns(_mockGiftAidCalculator.Object);

            _mockGiftAidCalculator.Setup(x => x.Calculate(100, "UK", taxData)).Returns(25);

            var giftAid = _giftAidOrchestrationService.CalculateGiftAid(100, "UK","General");

            giftAid.ShouldBe(25);
        }

        [Test]
        public void GivenSwimmingDonationandCountry_ReturnsGiftAid()
        {
            var taxData = new TaxData { Country = "UK", TaxRate = 20 };
            var taxList = new List<TaxData> { taxData };

            _taxRepository.Setup(x => x.GetTaxRate("UK"))
                .Returns(taxList);

            _giftAidCalculatorFinder.Setup(x => x.Find("Swimming")).Returns(_mockGiftAidCalculator.Object);

            _mockGiftAidCalculator.Setup(x => x.Calculate(100, "UK", taxData)).Returns(30);

            var giftAid = _giftAidOrchestrationService.CalculateGiftAid(100, "UK", "Swimming");

            giftAid.ShouldBe(30);
        }
    }
}
