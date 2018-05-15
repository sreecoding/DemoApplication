using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApplication.Controllers
{
    public interface IRequestValidator
    {
        Task<List<ErrorResponse>> Validate(decimal donationAmount, string countryCode, string eventType);
    }
}