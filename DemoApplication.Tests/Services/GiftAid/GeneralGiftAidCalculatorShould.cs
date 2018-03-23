using DemoApplication.Infrastructure.GiftAid;
using DemoApplication.Repositories;
using DemoApplication.Services;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services.GiftAid
{
    [TestFixture]
    public class GeneralGiftAidCalculatorShould
    {
        private IGiftAidCalculator _serviceUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _serviceUnderTest = new GeneralGiftAidCalculator();

        }

        [Test]
        public void GivenDonation_ReturnsGiftAid()
        {
            var taxData = new TaxData { Country = "UK", TaxRate = 20 };

            var giftAid = _serviceUnderTest.Calculate(100, "UK", taxData);

            giftAid.ShouldBe(25);
        }
    }
}
