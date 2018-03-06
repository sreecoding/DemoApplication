using DemoApplication.Controllers;
using DemoApplication.Services;
using Moq;
using NUnit.Framework;

namespace DemoApplication.Tests.Controllers
{
    [TestFixture]
    public class GiftAidControllerShould
    {
        private Mock<IGiftAidCalculationService> mockGiftAidService;
        private GiftAidController giftAidController;

        [Test]
        public void GivenDonation_ThenCallsGiftAidService()
        {
            mockGiftAidService = new Mock<IGiftAidCalculationService>();
            giftAidController = new GiftAidController(mockGiftAidService.Object);

            var giftAid = giftAidController.GetGiftAid(100, "UK");

            mockGiftAidService.Verify(x => x.CalculateGiftAid(100,"UK"), Times.Once);
        }
    }
}