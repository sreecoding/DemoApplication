using DemoApplication.Services;
using Moq;
using NUnit.Framework;

namespace DemoApplication.Tests
{
    [TestFixture]
    public class GiftAidCalculatorShould
    {
        private GiftAidCalculator _serviceUnderTest;
        private Mock<IGiftAidRepository> mockGiftAidRepository; 

        [SetUp]
        public void SetUp()
        {
            mockGiftAidRepository = new Mock<IGiftAidRepository>();

            _serviceUnderTest = new GiftAidCalculator(mockGiftAidRepository.Object);
        }

        [Test]
        public void GivenDonation_ReturnsGiftAid()
        {
            var giftAid = _serviceUnderTest.Calculate(100);

            Assert.IsNotNull(giftAid);
        }
    }
}
