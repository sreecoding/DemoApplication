using System;

namespace DemoApplication.Services
{
    public interface IGiftAidCalculator
    {
        Decimal Calculate(Decimal donationAmount);
    }
}