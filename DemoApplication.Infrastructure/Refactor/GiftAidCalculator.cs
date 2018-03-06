using System;

namespace DemoApplication.Services
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        private IGiftAidRepository _giftAidRepository;

        public GiftAidCalculator(IGiftAidRepository giftAidRepository)
        {
            _giftAidRepository = giftAidRepository;
        }

        public decimal Calculate(decimal donationAmount)
        {
            var giftPercent = _giftAidRepository.GetGiftPercentage();

            return (donationAmount * giftPercent)/100;
        }
    }

    public interface IGiftAidRepository
    {
        int GetGiftPercentage();

    }

    public class GiftAidRepository : IGiftAidRepository
    {
        public int GetGiftPercentage()
        {
            throw new NotImplementedException();
        }
    }
}