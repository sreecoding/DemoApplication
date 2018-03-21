using System;
using DemoApplication.Controllers;
using Shouldly;
using TechTalk.SpecFlow;

namespace DemoApplication.Specs
{
    [Binding]
    public class SaveTaxRateSteps
    {
        private int _taxRate;
        private string _country;
        //private CountryTaxRate _countryTaxRate;

        [Given(@"I have specified a Tax Rate of '(.*)'")]
        public void GivenIHaveSpecifiedATaxRateOf(int taxRate)
        {
            _taxRate = taxRate;
        }
        
        [Given(@"I have specified country as '(.*)'")]
        public void GivenIHaveSpecifiedCountryAs(string country)
        {
            _country = country;
        }
        
        [When(@"I Call Tax Controller")]
        public void WhenICallTaxController()
        {
            //var taxRateController = new TaxRateController(new TaxRateService)

            //_countryTaxRate = taxRateController.SaveTaxRate(new CountryTaxRate {Country = _country, TaxRate = _taxRate});
        }
        
        [Then(@"the new tax rate is added to the table")]
        public void ThenTheNewTaxRateIsAddedToTheTable()
        {
            //_countryTaxRate.TaxRate.ShouldBe(_taxRate);
            //_countryTaxRate.Country.ShouldBe(_country);
        }
    }

   
}
