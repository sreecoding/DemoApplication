using System;

namespace DemoApplication.Services
{
    public interface IGiftAidService
    {
        Decimal CalculateGiftAid(Decimal donationAmount);
    }
}