using System;
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

        public GiftAidController(IGiftAidOrchestrationService giftAidOrchestrationService)
        {
            _giftAidOrchestrationService = giftAidOrchestrationService;
        }

        [HttpGet]
        [Route("GiftAid/GetGiftAid")]
        public IHttpActionResult GetGiftAid(decimal donationAmount, string country)
        {
            var giftAidAmount = _giftAidOrchestrationService.CalculateGiftAid(donationAmount, country);

            return Ok(giftAidAmount);
        }


       // [SwaggerRequestExample(typeof(Donation), typeof(DonationModel))]
        //if (!ModelState.IsValid)
        //    return Content(HttpStatusCode.BadRequest, donation);
    }

    public class DonationModel : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Donation(1000, "UK");
        }
    }

    public class GiftAid
    {
        public Decimal GiftAmount;
    }
}
