using Swashbuckle.Examples;

namespace DemoApplication.Controllers
{
    public class DonationModel : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Donation(1000, "UK");
        }
    }
}