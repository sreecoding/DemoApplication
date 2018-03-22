using System;
using System.Collections.Generic;

namespace DemoApplication.Controllers
{

    public interface IRequestValidator
    {
        List<ErrorResponse> Validate(decimal donationAmount, string country, string eventType);
    }

    public class RequestValidator : IRequestValidator
    {

        public List<ErrorResponse> Validate(decimal donationAmount, string country, string eventType)
        {
            List<string> eventTypes = new List<string> {"General", "Swimming"};

            IList<ErrorResponse> errorResponses = new List<ErrorResponse>();

            if (String.IsNullOrEmpty(country))
                errorResponses.Add(new ErrorResponse("country", "Country Cannot be Empty") );

            if (donationAmount <= 0)
                errorResponses.Add(new ErrorResponse("donation", "Donation cannot be <= 0"));

            if (!eventTypes.Contains(eventType))
                errorResponses.Add(new ErrorResponse("event type", "Only supported event types are General and Swimming"));

            return (List<ErrorResponse>) errorResponses;
        }
        
    }
    
}