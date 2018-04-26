using System.Collections.Generic;
using DemoApplication.Domain;
using DemoApplication.Infrastructure.GiftAid;
using NUnit.Framework;

namespace DemoApplication.Services.Test.GiftAid
{
    [TestFixture]
    public class GiftAidCalculatorFinderShould
    {
        private IGiftAidCalculatorFinder _giftAidCalculatorFinder;

        
        [TestCase(GiftAidConstants.EventTypes.Swimming, ExpectedResult = typeof(SwimmingGiftAidCalculator))]
        [TestCase(GiftAidConstants.EventTypes.General, ExpectedResult = typeof(GeneralGiftAidCalculator))]
        public object ReturntheCalculatorMatchingEventType(string eventType)
        {
            _giftAidCalculatorFinder = new GiftAidCalculatorFinder(new List<IGiftAidCalculator>()
                { new GeneralGiftAidCalculator(), new SwimmingGiftAidCalculator()});

            var calculator = _giftAidCalculatorFinder.Find(eventType);

            return calculator.GetType();

        }
    }
}
