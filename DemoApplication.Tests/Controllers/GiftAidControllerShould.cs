using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http.Results;
using DemoApplication.Controllers;
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
            mockGiftAidService.Setup(x => x.CalculateGiftAid(100, "UK"))
                .Returns(25);

            giftAidController = new GiftAidController(mockGiftAidService.Object, new RequestValidator());
        }

        [Test]
        public void GivenDonation_ThenCalculatesGiftAid()
        {
            var giftAid = (OkNegotiatedContentResult<GiftAidResponse>)giftAidController.GetGiftAid(100, "UK");

            giftAid.Content.GiftAidAmount.ShouldBe(25);
        }

        [Test]
        public void GivenInvalidCountry_ReturnsValidationError()
        {
            var giftAid = (NegotiatedContentResult<List<ErrorResponse>>)giftAidController.GetGiftAid(1000, "");
            
            giftAid.StatusCode.ShouldBe(HttpStatusCode.BadRequest); 

        }

        [Test]
        public void GivenInvalidAmount_ReturnsValidationError()
        {
            var giftAid = (NegotiatedContentResult<List<ErrorResponse>>)giftAidController.GetGiftAid(0, "UK");

            giftAid.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }
    }
}