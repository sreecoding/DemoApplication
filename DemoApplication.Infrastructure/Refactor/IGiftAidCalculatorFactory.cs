namespace DemoApplication.Services
{
    public interface IGiftAidCalculatorFactory
    {
        IGiftAidCalculator Generate();
    }

    public class GiftAidCalculatorFactory : IGiftAidCalculatorFactory
    {
        public IGiftAidCalculator Generate()
        {
            //Use IOC ?
            return new GiftAidCalculator(new TaxRepository());
        }
    }
}