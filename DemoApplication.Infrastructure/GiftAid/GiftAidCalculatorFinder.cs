﻿using System.Collections.Generic;
using System.Linq;
using DemoApplication.Services;

namespace DemoApplication.Infrastructure.GiftAid
{
    public interface IGiftAidCalculatorFinder
    {
        IGiftAidCalculator Find(string eventType);
    }

    public class GiftAidCalculatorFinder : IGiftAidCalculatorFinder
    {
        private List<IGiftAidCalculator> _giftAidCalculators;

        public GiftAidCalculatorFinder(IEnumerable<IGiftAidCalculator> giftAidCalculators)
        {
            _giftAidCalculators = new List<IGiftAidCalculator>(giftAidCalculators);
        }

        public IGiftAidCalculator Find(string eventType)
        {
            var giftAidCalculator = _giftAidCalculators.First(c => c.MatchEvent(eventType));

            return giftAidCalculator;
        }
    }
}