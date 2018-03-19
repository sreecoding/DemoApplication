using System.Net;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;

namespace DemoApplication.Tests.Controllers
{
    [TestFixture]
    public class TaxRateControllerShould
    {
        private Mock<ITaxRateService> _mockTaxRateService;

        [Test]
        public void GivenInvalidTaxRate_ReturnsBadRequest()
        {
            _mockTaxRateService = new Mock<ITaxRateService>();

            //var taxRateController = new TaxRateController(_mockTaxRateService.Object);

            //taxRateController.ModelState.AddModelError("Tax Rate","Tax Rate should be > 0" );

            //var result = (NegotiatedContentResult<CountryTaxRate>)taxRateController.SaveTaxRate(new CountryTaxRate() {Country = "UK", TaxRate = 0});

           // result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        public void GivenTaxRate_ReturnsSavedValue()
        {
            _mockTaxRateService = new Mock<ITaxRateService>();

           // var taxRateController = new TaxRateController(_mockTaxRateService.Object);


        }

    }

}
