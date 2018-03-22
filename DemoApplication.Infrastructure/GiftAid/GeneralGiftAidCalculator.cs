using System;
using System.Linq;
using DemoApplication.Repositories;

namespace DemoApplication.Services
{
    public class GeneralGiftAidCalculator : IGiftAidCalculator
    {

       public decimal Calculate(decimal donationAmount,string country,TaxData giftPercent)
        {
            return (donationAmount * giftPercent.TaxRate)/(100 - giftPercent.TaxRate);
        }

        public bool MatchEvent(string eventType)
        {
            return eventType == "General";
        }
    }
}