using DemoApplication.Controllers;

namespace DemoApplication.Tests.Controllers
{
    public interface ITaxRateService
    {
        CountryTaxRate SaveTaxRate(CountryTaxRate taxRate);
    }

    public class TaxRateService : ITaxRateService
    {
        public CountryTaxRate SaveTaxRate(CountryTaxRate taxRate)
        {
            throw new System.NotImplementedException();
        }
    }
}