using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DemoApplication.Services;

namespace DemoApplication.Controllers.GiftAidController
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
        public IHttpActionResult GetGiftAid(decimal donationAmount, string country, string eventType)
        {
            var validationErrors = _requestValidator.Validate(donationAmount, country);

            if (validationErrors.Any())
                return Content(HttpStatusCode.BadRequest, validationErrors);

            var giftAidAmount = _giftAidOrchestrationService.CalculateGiftAid(donationAmount, country, eventType);

            return Ok(new GiftAidResponse {GiftAidAmount = giftAidAmount});
        }
    }
  

  
}
