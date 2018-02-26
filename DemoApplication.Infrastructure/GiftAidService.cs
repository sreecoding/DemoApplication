using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Infrastructure
{
    public class GiftAidService : IGiftAidService
    {
        private readonly IGiftAidCalculatorFactory _giftAidCalculatorFactory;

        public GiftAidService(IGiftAidCalculatorFactory giftAidCalculatorFactory)
        {
            this._giftAidCalculatorFactory = giftAidCalculatorFactory;
        }

        public decimal CalculateGiftAid(decimal donationAmount)
        {
            var giftAidCalculator = _giftAidCalculatorFactory.Generate();

            return giftAidCalculator.Calculate(donationAmount);
        }
    }
}
