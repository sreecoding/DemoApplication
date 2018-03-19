using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http;

namespace DemoApplication.Controllers
{
    [RoutePrefix("api")]
    public class TaxRateController : ApiController
    {
        private readonly ITaxRateService _taxRateService;

        public TaxRateController(ITaxRateService taxRateService)
        {
            _taxRateService = taxRateService;
        }

        [HttpPost]
        [Route("TaxRate/SaveTaxRate")]
        public IHttpActionResult SaveTaxRate(CountryTaxRate countryTaxRate)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, countryTaxRate);



            return Ok(countryTaxRate);
        }
    }

    public interface ITaxRateService
    {

    }

    public class CountryTaxRate
    {
        [Required]
        [StringLength(2,MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        [Range(1,100000)]
        public Decimal TaxRate { get; set; }
    }
}