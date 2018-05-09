using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DemoApplication.Services.GiftAid;

namespace DemoApplication.Controllers.GiftAid
{
   [RoutePrefix("api")]
    public class GiftAidController : ApiController
    {
        private readonly IGiftAidOrchestrationService _giftAidOrchestrationService;
        private readonly IRequestValidator _requestValidator;

        public GiftAidController(IGiftAidOrchestrationService giftAidOrchestrationService, IRequestValidator requestValidator)
        {
            _giftAidOrchestrationService = giftAidOrchestrationService;
            _requestValidator = requestValidator;
        }

        /// <summary>
        /// Get Gift Aid Value
        /// </summary>
        /// <remarks>
        /// Calculates the GiftAid Amount given the country, donation amount and Event
        /// </remarks>
        /// <param name="donationAmount"> Amount Donated </param>
        /// <param name="country"> Country </param>
        /// <param name="eventType"> Type Of Event "Swimming", "General"</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("GiftAid/GetGiftAid")]
        [ResponseType(typeof(GiftAidResponse))]
        public async Task<IHttpActionResult> GetGiftAid(decimal donationAmount, string country, string eventType)
        {
            var validationErrors = _requestValidator.Validate(donationAmount, country,eventType);

            if (validationErrors.Any())
                return Content(HttpStatusCode.BadRequest, new GiftAidErrorResponse(validationErrors));

            var giftAidAmount = await _giftAidOrchestrationService.CalculateGiftAid(donationAmount, country, eventType);

            return Content(HttpStatusCode.OK,new GiftAidResponse(giftAidAmount));
        }
    }

    public class GiftAidErrorResponse
    {
        public GiftAidErrorResponse(List<ErrorResponse> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IList<ErrorResponse> ValidationErrors { get; }
    }
}
