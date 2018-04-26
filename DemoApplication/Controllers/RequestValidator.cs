using System;
using System.Collections.Generic;
using System.Linq;
using DemoApplication.Domain;
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
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Country, $"{GiftAidConstants.InputFields.Country} Cannot be Empty") );

            if (donationAmount <= 0)
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Donation, $"{GiftAidConstants.InputFields.Donation} cannot be <= 0"));

            if (!eventTypes.Contains(eventType))
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.EventType, $"Only supported event types are {String.Join(",",eventTypes)}"));

            return (List<ErrorResponse>) errorResponses;

        }
        
    }
    
}