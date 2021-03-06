using DemoApplication.Services.GiftAid;
using DemoApplication.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Services.Test.GiftAid
{
    [TestFixture]
    public class SwimmingGiftAidCalculatorShould
    {
        private IGiftAidCalculator _serviceUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _serviceUnderTest = new SwimmingGiftAidCalculator();

        }

        [Test]
        public void GivenDonation_ReturnsGiftAid()
        {
            var taxData = new TaxData { Country = "UK", TaxRate = 20 };

            var giftAid = _serviceUnderTest.Calculate(100, "UK", taxData);

            giftAid.ShouldBe(30);
        }
    }
}
