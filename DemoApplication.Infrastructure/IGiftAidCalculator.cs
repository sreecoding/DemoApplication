using System;

namespace DemoApplication.Infrastructure
{
    public interface IGiftAidCalculator
    {
        Decimal Calculate(Decimal donationAmount);
    }
}