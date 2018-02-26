using DemoApplication.Infrastructure;
using Moq;
using NUnit.Framework;

namespace DemoApplication.Tests
{
    [TestFixture]
    public class GiftAidCalculatorShould
    {
        private GiftAidCalculator _serviceUnderTest;

        [SetUp]
        public void SetUp()
        { 
            _serviceUnderTest = new GiftAidCalculator();
        }

        [Test]
        public void GivenDonation_ReturnsGiftAid()
        {
            var giftAid = _serviceUnderTest.Calculate(100);

            Assert.IsNotNull(giftAid);
        }
    }
}
