using System;
using System.Collections.Generic;
using DemoApplication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Services
{
    [TestFixture]
    public class GiftAidCalculatonServiceShould
    {
        private IGiftAidCalculationService _giftAidCalculationService;
        private Mock<ITaxRepository> _mockTaxRepository;

        [Test]
        public void GivenDonationandCountry_ReturnsGiftAid()
        {
            _mockTaxRepository = new Mock<ITaxRepository>();
            _giftAidCalculationService = new GiftAidCalculationService(_mockTaxRepository.Object);

            _mockTaxRepository.Setup(x => x.GetTaxRate("UK")).Returns(new List<TaxData> { new TaxData { TaxRate = 20 } });

            var giftAid = _giftAidCalculationService.CalculateGiftAid(100, "UK");
            giftAid.ShouldBe(25);

        }
    }
}
