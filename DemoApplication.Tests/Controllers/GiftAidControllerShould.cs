using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.GiftAidController;
using DemoApplication.Domain;
using DemoApplication.Infrastructure.GiftAid;
using DemoApplication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Tests.Controllers
{
    [TestFixture]
    public class GiftAidControllerShould
    {
        private Mock<IGiftAidOrchestrationService> _mockGiftAidService;
        private GiftAidController _giftAidController;
        private List<IGiftAidCalculator> _giftAidCalculators;

        [SetUp]
        public void Setup()
        {
            _giftAidCalculators = new List<IGiftAidCalculator>() { new GeneralGiftAidCalculator(), new SwimmingGiftAidCalculator() };
            _mockGiftAidService = new Mock<IGiftAidOrchestrationService>();
            _giftAidController = new GiftAidController(_mockGiftAidService.Object, 
                              new RequestValidator(_giftAidCalculators));
        }

        [Test]
        [TestCase(GiftAidConstants.EventTypes.General,25)]
        [TestCase(GiftAidConstants.EventTypes.Swimming, 30)]
        public async Task GivenDonation_ThenCalculatesGiftAid(string eventType, Decimal giftAidValue)
        {
            _mockGiftAidService.Setup(x => x.CalculateGiftAid(100, "UK", eventType))
                .Returns(Task.FromResult(giftAidValue));

            var giftAid = (NegotiatedContentResult<GiftAidResponse>)await _giftAidController.GetGiftAid(100, "UK",eventType);

            giftAid.Content.GiftAidAmount.ShouldBe(giftAidValue);
            giftAid.Content.ValidationErrors.Count.ShouldBe(0);
        }

        [Test]
        [TestCase(1000,"", GiftAidConstants.EventTypes.General)]
        [TestCase(0,"UK", GiftAidConstants.EventTypes.General)]
        [TestCase(1000,"UK","Invalid")]
        public async Task GivenInvalidInputs_ReturnsValidationError(int donationAmount, string country, string eventType)
        {
            var giftAidResponse = (NegotiatedContentResult<GiftAidResponse>) await _giftAidController.GetGiftAid(donationAmount, country, eventType);

            giftAidResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var error = giftAidResponse.Content.ValidationErrors.Single();

            error.ErrorMessage.ShouldNotBeNullOrEmpty();
            error.ParameterName.ShouldNotBeNullOrEmpty();
        }
    }
}