using System.Threading.Tasks;

namespace DemoApplication.Services.GiftAid
{
    public interface IGiftAidOrchestrationService
    {
        Task<decimal> CalculateGiftAid(decimal donationAmount, string country, string eventType);
    }
}