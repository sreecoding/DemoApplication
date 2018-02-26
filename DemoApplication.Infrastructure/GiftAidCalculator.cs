using System;

namespace DemoApplication.Infrastructure
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        public decimal Calculate(decimal donationAmount)
        {
            return donationAmount / 5;
        }
    }
}