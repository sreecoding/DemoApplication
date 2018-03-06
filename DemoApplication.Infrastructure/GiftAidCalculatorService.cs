using System;
using System.Linq;
using Moq;

namespace DemoApplication.Services
{
    public interface IGiftAidCalculationService
    {
        decimal CalculateGiftAid(decimal donationAmount,string country);
    }

    public class GiftAidCalculationService : IGiftAidCalculationService
    {
        private ITaxRepository taxRepository;

        public GiftAidCalculationService(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public decimal CalculateGiftAid(decimal donationAmount, string country)
        {
            var tax = taxRepository.GetTaxRate(country).Single();
            var giftAid = (donationAmount * tax.TaxRate) / (100 - tax.TaxRate);

            return giftAid;
        }
    }
   
}