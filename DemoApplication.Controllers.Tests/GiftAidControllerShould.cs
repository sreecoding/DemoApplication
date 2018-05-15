using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DemoApplication.Controllers.GiftAid;
using DemoApplication.Domain;
using DemoApplication.Services.GiftAid;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Controllers.Tests
{
    [TestFixture]
    public class GiftAidControllerShould
    {
        private Mock<IGiftAidOrchestrationService> _mockGiftAidService;
        private GiftAidController _giftAidController;
        private Mock<IRequestValidator> _mockRequestValidator;

        [SetUp]
        public void Setup()
        {
            _mockRequestValidator = new Mock<IRequestValidator>();
            _mockGiftAidService = new Mock<IGiftAidOrchestrationService>();
            _giftAidController = new GiftAidController(_mockGiftAidService.Object,_mockRequestValidator.Object);
        }

        [TestCase(GiftAidConstants.EventTypes.General,25)]
        [TestCase(GiftAidConstants.EventTypes.Swimming, 30)]
        public async Task GivenDonation_ThenCalculatesGiftAid(string eventType, Decimal giftAidValue)
        {
            _mockGiftAidService.Setup(x => x.CalculateGiftAid(100, "UK", eventType))
                .Returns(Task.FromResult(giftAidValue));

            _mockRequestValidator.Setup(x => x.Validate(100, "UK", eventType))
                                .Returns(Task.FromResult(new List<ErrorResponse>()));

            var giftAid = (NegotiatedContentResult<GiftAidResponse>)await _giftAidController.GetGiftAid(100, "UK",eventType);

            giftAid.Content.GiftAidAmount.ShouldBe(giftAidValue);
        }

        [TestCase(1000,"", GiftAidConstants.EventTypes.General)]
        [TestCase(0,"UK", GiftAidConstants.EventTypes.General)]
        [TestCase(1000,"UK","Invalid")]
        public async Task GivenInvalidInputs_ReturnsBadRequest(int donationAmount, string country, string eventType)
        {
            _mockRequestValidator.Setup(x => x.Validate(donationAmount, country, eventType))
                .Returns(Task.FromResult(new List<ErrorResponse>(){new ErrorResponse("Test Error Message","Test Field")}));

            var giftAidResponse = (NegotiatedContentResult<GiftAidErrorResponse>) await _giftAidController.GetGiftAid(donationAmount, country, eventType);

            giftAidResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [TestCase(1000, "", GiftAidConstants.EventTypes.General)]
        [TestCase(0, "UK", GiftAidConstants.EventTypes.General)]
        [TestCase(1000, "UK", "Invalid")]
        public async Task GivenInvalidInputs_ReturnsErrorMessages(int donationAmount, string country, string eventType)
        {
            const string testErrorMessage = "Test Error Message";
            const string parameterName = "Test Field";

            _mockRequestValidator.Setup(x => x.Validate(donationAmount, country, eventType))
                .Returns(Task.FromResult(new List<ErrorResponse>() { new ErrorResponse(parameterName,testErrorMessage) }));

            var giftAidResponse = (NegotiatedContentResult<GiftAidErrorResponse>)await _giftAidController.GetGiftAid(donationAmount, country, eventType);

            var error = giftAidResponse.Content.ValidationErrors.Single();
            error.ErrorMessage.ShouldBe(testErrorMessage);
            error.ParameterName.ShouldBe(parameterName);
        }

    }
}