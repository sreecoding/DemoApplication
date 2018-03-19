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

            giftAidController = new GiftAidController(mockGiftAidService.Object);
        }

        [Test]
        public void GivenDonation_ThenCalculatesGiftAid()
        {
            var giftAid = (OkNegotiatedContentResult<decimal>)giftAidController.GetGiftAid(100, "UK");

            giftAid.Content.ShouldBe(25);
        }

        [Test]
        public void GivenInvalidModelState_ReturnsValidationError()
        {
            giftAidController.ModelState.AddModelError("countryError","Country Code Not Valid");

            var giftAid = (NegotiatedContentResult<Donation>)giftAidController.GetGiftAid(5, "");
            
            giftAid.StatusCode.ShouldBe(HttpStatusCode.BadRequest); 

        }
    }
}