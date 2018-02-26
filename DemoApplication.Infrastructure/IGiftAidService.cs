using System;

namespace DemoApplication.Infrastructure
{
    public interface IGiftAidService
    {
        Decimal CalculateGiftAid(Decimal donationAmount);
    }
}