using System;
using DemoApplication.Repositories;

namespace DemoApplication.Services.GiftAid
{
    public interface IGiftAidCalculator
    {
        Decimal Calculate(Decimal donationAmount, string country, TaxData giftTax);


        bool MatchEvent(string eventType);

        string GetGiftAidType();
    }
}