using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Repositories;
using DemoApplication.Services;

namespace DemoApplication.Infrastructure.GiftAid
{
    public class SwimmingGiftAidCalculator : IGiftAidCalculator
    {
        public decimal Calculate(decimal donationAmount, string country, TaxData giftTax)
        {
            var giftAidAmount = (donationAmount * giftTax.TaxRate) / (100 - giftTax.TaxRate);

            return giftAidAmount + (donationAmount * 5 / 100);
        }

        public bool MatchEvent(string eventType)
        {
            return eventType == "Swimming";
        }
    }
}
