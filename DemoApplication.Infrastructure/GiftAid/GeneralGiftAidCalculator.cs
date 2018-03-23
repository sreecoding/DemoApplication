using DemoApplication.Domain;
using DemoApplication.Repositories;
using DemoApplication.Services;

namespace DemoApplication.Infrastructure.GiftAid
{
    public class GeneralGiftAidCalculator : IGiftAidCalculator
    {

       public decimal Calculate(decimal donationAmount,string country,TaxData giftPercent)
        {
            return (donationAmount * giftPercent.TaxRate)/(100 - giftPercent.TaxRate);
        }

        public bool MatchEvent(string eventType)
        {
            return eventType == GetGiftAidType();
        }

        public string GetGiftAidType()
        {
            return GiftAidConstants.EventTypes.General;
        }
    }
}