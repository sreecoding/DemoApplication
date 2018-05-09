namespace DemoApplication.Services.GiftAid
{
    public interface IGiftAidCalculatorFinder
    {
        IGiftAidCalculator Find(string eventType);
    }
}