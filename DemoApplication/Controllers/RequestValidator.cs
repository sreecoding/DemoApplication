using System;
using System.Collections.Generic;
using System.Linq;
using DemoApplication.Domain;
using DemoApplication.Services.GiftAid;

namespace DemoApplication.Controllers
{
    public class RequestValidator : IRequestValidator
    {
        private readonly IList<IGiftAidCalculator> _giftAidCalculators;
        private readonly ICountryService _countryService;

        public RequestValidator(IList<IGiftAidCalculator> giftAidCalculators, ICountryService countryService)
        {
            _giftAidCalculators = giftAidCalculators;
            _countryService = countryService;
        }

        public List<ErrorResponse> Validate(decimal donationAmount, string countryCode, string eventType)
        {
            var eventTypes = _giftAidCalculators.Select(x => x.GetGiftAidType()).ToList();

            IList<ErrorResponse> errorResponses = new List<ErrorResponse>();

            if (String.IsNullOrEmpty(countryCode)) 
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Country, $"{GiftAidConstants.InputFields.Country} Cannot be Empty") );

            if (donationAmount <= 0)
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Donation, $"{GiftAidConstants.InputFields.Donation} cannot be <= 0"));

            if (!eventTypes.Contains(eventType))
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.EventType, $"Only supported event types are {String.Join(",",eventTypes)}"));

            var country = _countryService.GetCountryByCountryCode(countryCode);

            if (country == null || country.CountryCode != countryCode)
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Country, $"{GiftAidConstants.InputFields.Country} is not a valid country code"));

            return (List<ErrorResponse>) errorResponses;

        }
        
    }
    
}