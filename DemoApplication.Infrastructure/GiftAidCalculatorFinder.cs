using System.Collections.Generic;
using System.Linq;
using DemoApplication.Infrastructure.GiftAid;
using DemoApplication.Services;

namespace DemoApplication.Infrastructure
{
    public interface IGiftAidCalculatorFinder
    {
        IGiftAidCalculator Find(string eventType);
    }

    public class GiftAidCalculatorFinder : IGiftAidCalculatorFinder
    {
        private List<IGiftAidCalculator> _giftAidCalculators;

        public IGiftAidCalculator Find(string eventType)
        {
            _giftAidCalculators = new List<IGiftAidCalculator>() {new GeneralGiftAidCalculator(), new SwimmingGiftAidCalculator()};

            var giftAidCalculator = _giftAidCalculators.First(c => c.MatchEvent(eventType));

            return giftAidCalculator;
        }
    }
}