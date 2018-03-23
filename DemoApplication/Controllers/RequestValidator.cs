using System;
using System.Collections.Generic;
using System.Linq;
using DemoApplication.Services;

namespace DemoApplication.Controllers
{
    public interface IRequestValidator
    {
        List<ErrorResponse> Validate(decimal donationAmount, string country, string eventType);
    }

    public class RequestValidator : IRequestValidator
    {
        private readonly IList<IGiftAidCalculator> _giftAidCalculators;

        public RequestValidator(IList<IGiftAidCalculator> giftAidCalculators)
        {
            _giftAidCalculators = giftAidCalculators;
        }

        public List<ErrorResponse> Validate(decimal donationAmount, string country, string eventType)
        {
            var eventTypes = _giftAidCalculators.Select(x => x.GetGiftAidType()).ToList();

            IList<ErrorResponse> errorResponses = new List<ErrorResponse>();

            if (String.IsNullOrEmpty(country))
                errorResponses.Add(new ErrorResponse("country", "Country Cannot be Empty") );

            if (donationAmount <= 0)
                errorResponses.Add(new ErrorResponse("donation", "Donation cannot be <= 0"));

            if (!eventTypes.Contains(eventType))
                errorResponses.Add(new ErrorResponse("eventtype", "Only supported event types are General and Swimming"));

            return (List<ErrorResponse>) errorResponses;

        }
        
    }
    
}