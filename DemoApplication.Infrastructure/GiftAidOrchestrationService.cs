using System.Collections.Generic;
using System.Linq;
using DemoApplication.Infrastructure;
using DemoApplication.Repositories;

namespace DemoApplication.Services
{
    public interface IGiftAidOrchestrationService
    {
        decimal CalculateGiftAid(decimal donationAmount, string country, string eventType);
    }

    public class GiftAidOrchestrationService : IGiftAidOrchestrationService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IGiftAidCalculatorFinder _giftAidCalculatorFinder;

        public GiftAidOrchestrationService(ITaxRepository taxRepository, IGiftAidCalculatorFinder giftAidCalculatorFinder)
        {
            _taxRepository = taxRepository;
            _giftAidCalculatorFinder = giftAidCalculatorFinder;
        }

        public decimal CalculateGiftAid(decimal donationAmount, string country, string eventType)
        {
            var taxList = _taxRepository.GetTaxRate(country).ToList();

            if (!taxList.Any())
                return 0;

            var giftAidCalculator = _giftAidCalculatorFinder.Find(eventType);

            return giftAidCalculator.Calculate(donationAmount, country, taxList.Single());
        }
    }
   
}