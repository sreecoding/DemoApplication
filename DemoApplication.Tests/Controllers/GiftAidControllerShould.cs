using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.GiftAidController;
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
        private Mock<IGiftAidOrchestrationService> mockGiftAidService;
        private GiftAidController giftAidController;

        [SetUp]
        public void Setup()
        {
            mockGiftAidService = new Mock<IGiftAidOrchestrationService>();
            mockGiftAidService.Setup(x => x.CalculateGiftAid(100, "UK","General"))
                .Returns(25);

            giftAidController = new GiftAidController(mockGiftAidService.Object, new RequestValidator());
        }

        [Test]
        [TestCase("General")]
        public void GivenDonation_ThenCalculatesGiftAid(string eventType)
        {
            var giftAid = (OkNegotiatedContentResult<GiftAidResponse>)giftAidController.GetGiftAid(100, "UK",eventType);

            giftAid.Content.GiftAidAmount.ShouldBe(25);
        }

        [Test]
        public void GivenDonationForSwimming_ThenCalculatesGiftAid()
        {
            mockGiftAidService.Setup(x => x.CalculateGiftAid(100, "UK", "Swimming"))
                .Returns(30);

            var giftAid = (OkNegotiatedContentResult<GiftAidResponse>)giftAidController.GetGiftAid(100, "UK", "Swimming");

          

            giftAid.Content.GiftAidAmount.ShouldBe(30);
        }

        [Test]
        public void GivenInvalidCountry_ReturnsValidationError()
        {
            var giftAid = (NegotiatedContentResult<List<ErrorResponse>>)giftAidController.GetGiftAid(1000, "", "General");
            
            giftAid.StatusCode.ShouldBe(HttpStatusCode.BadRequest); 

        }

        [Test]
        public void GivenInvalidAmount_ReturnsValidationError()
        {
            var giftAid = (NegotiatedContentResult<List<ErrorResponse>>)giftAidController.GetGiftAid(0, "UK", "General");

            giftAid.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }
    }
}