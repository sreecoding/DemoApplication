using System.Collections.Generic;
using DemoApplication.Infrastructure.GiftAid;
using DemoApplication.Services;
using NUnit.Framework;

namespace DemoApplication.Tests.Services.GiftAid
{
    [TestFixture]
    public class GiftAidCalculatorFinderShould
    {
        private IGiftAidCalculatorFinder _giftAidCalculatorFinder;

        [Test]
        [TestCase("Swimming", ExpectedResult = typeof(SwimmingGiftAidCalculator))]
        [TestCase("General", ExpectedResult = typeof(GeneralGiftAidCalculator))]
        public object ReturntheCalculatorMatchingEventtype(string eventType)
        {
            _giftAidCalculatorFinder = new GiftAidCalculatorFinder(new List<IGiftAidCalculator>()
                { new GeneralGiftAidCalculator(), new SwimmingGiftAidCalculator()});

            var calculator = _giftAidCalculatorFinder.Find(eventType);

            return calculator.GetType();

        }
    }
}
