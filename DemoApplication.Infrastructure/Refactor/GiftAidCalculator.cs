using System;
using System.Linq;
using DemoApplication.Repositories;

namespace DemoApplication.Services
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        private readonly ITaxRepository _taxRepository;

        public GiftAidCalculator(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public decimal Calculate(decimal donationAmount,string country)
        {
            var giftPercent = _taxRepository.GetTaxRate(country).Single();

            return (donationAmount * giftPercent.TaxRate)/(100 - giftPercent.TaxRate);
        }
    }

   

   
}