using System.Collections.Generic;
using DemoApplication.Services;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services
{
    [TestFixture]
    public class GiftAidServiceShould
    {
        private IGiftAidOrchestrationService _giftAidOrchestrationService;
        private Mock<IGiftAidCalculator> _mockGiftAidCalculator;

        [Test]
        public void GivenDonationandCountry_ReturnsGiftAid()
        {
            _mockGiftAidCalculator = new Mock<IGiftAidCalculator>();
            _giftAidOrchestrationService = new GiftAidOrchestrationService(_mockGiftAidCalculator.Object);
            _mockGiftAidCalculator.Setup(x => x.Calculate(100, "UK")).Returns(25);

            var giftAid = _giftAidOrchestrationService.CalculateGiftAid(100, "UK");

            giftAid.ShouldBe(25);

        }
    }
}
