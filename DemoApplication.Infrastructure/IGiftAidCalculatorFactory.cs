namespace DemoApplication.Infrastructure
{
    public interface IGiftAidCalculatorFactory
    {
        IGiftAidCalculator Generate();
    }

    public class GiftAidCalculatorFactory : IGiftAidCalculatorFactory
    {
        public IGiftAidCalculator Generate()
        {
            return new GiftAidCalculator();
        }
    }
}