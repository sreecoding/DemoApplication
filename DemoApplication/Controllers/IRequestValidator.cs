using System.Collections.Generic;

namespace DemoApplication.Controllers
{
    public interface IRequestValidator
    {
        List<ErrorResponse> Validate(decimal donationAmount, string countryCode, string eventType);
    }
}