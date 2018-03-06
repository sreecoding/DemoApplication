using System;
using System.Web.Http;
using DemoApplication.Services;

namespace DemoApplication.Controllers
{
    [RoutePrefix("api")]
    public class GiftAidController
    {
        private readonly IGiftAidCalculationService _giftAidService;

        public GiftAidController(IGiftAidCalculationService giftAidService)
        {
            _giftAidService = giftAidService;
        }

        public decimal GetGiftAid(decimal donationAmount, string country)
        {
            return _giftAidService.CalculateGiftAid(donationAmount,country);
        }
    }

    public class GiftAid
    {
        public Decimal GiftAmount;
    }
}
