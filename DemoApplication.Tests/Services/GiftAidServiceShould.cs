using DemoApplication.Services;
using Moq;
using NUnit.Framework;

namespace DemoApplication.Tests.Services
{
    [TestFixture]
    public class GiftAidServiceShould
    {
        private Mock<IGiftAidCalculatorFactory> mockGiftAidCalculatorFactory;
        private Mock<IGiftAidCalculator> mockGiftAidCalculator;
        private IGiftAidService serviceUnderTest;

        [Test]
        public void GivenDonation_CalculatesGiftAid()
        {
            mockGiftAidCalculator = new Mock<IGiftAidCalculator>();
            mockGiftAidCalculatorFactory = new Mock<IGiftAidCalculatorFactory>();

            mockGiftAidCalculatorFactory.Setup(x => x.Generate())
                .Returns(mockGiftAidCalculator.Object);

            serviceUnderTest = new GiftAidService(mockGiftAidCalculatorFactory.Object);

            var giftAid = serviceUnderTest.CalculateGiftAid(100);

            mockGiftAidCalculator.Verify(x=>x.Calculate(100),Times.Once);


        }
    }
}
