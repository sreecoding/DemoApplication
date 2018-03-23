using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DemoApplication.Controllers;
using DemoApplication.Controllers.GiftAidController;
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
        private Mock<IGiftAidOrchestrationService> mockGiftAidService;
        private GiftAidController giftAidController;

        [SetUp]
        public void Setup()
        {
            mockGiftAidService = new Mock<IGiftAidOrchestrationService>();
            giftAidController = new GiftAidController(mockGiftAidService.Object, new RequestValidator());
        }

        [Test]
        [TestCase("General",25)]
        [TestCase("Swimming",30)]
        public async Task GivenDonation_ThenCalculatesGiftAid(string eventType, Decimal giftAidValue)
        {
            mockGiftAidService.Setup(x => x.CalculateGiftAid(100, "UK", eventType))
                .Returns(Task.FromResult(giftAidValue));

            var giftAid = (NegotiatedContentResult<GiftAidResponse>)await giftAidController.GetGiftAid(100, "UK",eventType);

            giftAid.Content.GiftAidAmount.ShouldBe(giftAidValue);
            giftAid.Content.ValidationErrors.Count.ShouldBe(0);
        }

        [Test]
        [TestCase(1000,"","General")]
        [TestCase(0,"UK","General")]
        [TestCase(1000,"UK","Invalid")]
        public async Task GivenInvalidInputs_ReturnsValidationError(int donationAmount, string country, string eventType)
        {
            var giftAidResponse = (NegotiatedContentResult<GiftAidResponse>) await giftAidController.GetGiftAid(donationAmount, country, eventType);

            giftAidResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var error = giftAidResponse.Content.ValidationErrors.Single();

            error.ErrorMessage.ShouldNotBeNullOrEmpty();
            error.ParameterName.ShouldNotBeNullOrEmpty();

        }
    }
}