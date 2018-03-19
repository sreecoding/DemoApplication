using System.Collections.Generic;
using DemoApplication.Repositories;
using DemoApplication.Services;
using Moq;
using NUnit.Framework;

namespace DemoApplication.Tests
{
    [TestFixture]
    public class GiftAidCalculatorShould
    {
        private GiftAidCalculator _serviceUnderTest;
        private Mock<ITaxRepository> mockTaxRepository; 

        [SetUp]
        public void SetUp()
        {
            mockTaxRepository = new Mock<ITaxRepository>();

            mockTaxRepository.Setup(x => x.GetTaxRate("UK"))
                .Returns(new List<TaxData>
                    { new TaxData{TaxRate = 20}});

            _serviceUnderTest = new GiftAidCalculator(mockTaxRepository.Object);
        }

        [Test]
        public void GivenDonation_ReturnsGiftAid()
        {
            var giftAid = _serviceUnderTest.Calculate(100,"UK");

            Assert.IsNotNull(giftAid);
        }
    }
}
