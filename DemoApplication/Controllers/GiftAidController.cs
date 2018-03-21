using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DemoApplication.Services;
using Swashbuckle.Examples;

namespace DemoApplication.Controllers
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

        [HttpGet]
        [Route("GiftAid/GetGiftAid")]
        public IHttpActionResult GetGiftAid(decimal donationAmount, string country)
        {

            var validationErrors = _requestValidator.Validate(donationAmount, country);

            if (validationErrors.Any())
                return Content(HttpStatusCode.BadRequest, validationErrors);

            var giftAidAmount = _giftAidOrchestrationService.CalculateGiftAid(donationAmount, country);

            return Ok(new GiftAidResponse {GiftAidAmount = giftAidAmount});
        }
    }
  

  
}
