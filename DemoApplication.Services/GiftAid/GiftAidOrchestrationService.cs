using System.Linq;
using System.Threading.Tasks;
using DemoApplication.Repositories;

namespace DemoApplication.Services.GiftAid
{
    public class GiftAidOrchestrationService : IGiftAidOrchestrationService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IGiftAidCalculatorFinder _giftAidCalculatorFinder;

        public GiftAidOrchestrationService(ITaxRepository taxRepository, IGiftAidCalculatorFinder giftAidCalculatorFinder)
        {
            _taxRepository = taxRepository;
            _giftAidCalculatorFinder = giftAidCalculatorFinder;
        }

        public async Task<decimal> CalculateGiftAid(decimal donationAmount, string country, string eventType)
        {
            var taxList = await _taxRepository.GetTaxRate(country);

            if (!taxList.Any())
                return 0;

            var giftAidCalculator = _giftAidCalculatorFinder.Find(eventType);

            return giftAidCalculator.Calculate(donationAmount, country, taxList.Single());
        }
    }
   
}