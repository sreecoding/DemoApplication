using System;
using DemoApplication.Repositories;

namespace DemoApplication.Services
{
    public interface IGiftAidCalculator
    {
        Decimal Calculate(Decimal donationAmount, string country, TaxData giftTax);


        bool MatchEvent(string eventType);
    }
}