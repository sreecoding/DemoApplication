using System.Linq;

namespace DemoApplication.Services
{
    public interface IGiftAidOrchestrationService
    {
        decimal CalculateGiftAid(decimal donationAmount,string country);
    }

    public class GiftAidOrchestrationService : IGiftAidOrchestrationService
    {
        private IGiftAidCalculator _giftAidCalculator;

        public GiftAidOrchestrationService(IGiftAidCalculator giftAidCalculator)
        {
            _giftAidCalculator = giftAidCalculator;
        }

        public decimal CalculateGiftAid(decimal donationAmount, string country)
        {
            var giftAid = _giftAidCalculator.Calculate(donationAmount,country);

            return giftAid;
        }
    }
   
}