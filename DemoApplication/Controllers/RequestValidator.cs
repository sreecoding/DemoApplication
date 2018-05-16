using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApplication.Domain;
using DemoApplication.Services.GiftAid;
using DemoApplication.Services.GiftAid.Interfaces;

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

        public async Task<List<ErrorResponse>> Validate(decimal donationAmount, string countryCode, string eventType)
        {
            IList<ErrorResponse> errorResponses = new List<ErrorResponse>();

            if (donationAmount <= 0)
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Donation, $"{GiftAidConstants.InputFields.Donation} cannot be <= 0"));

            ValidateEventTypes(eventType, errorResponses);

            await ValidateCountry(countryCode, errorResponses);

            return (List<ErrorResponse>) errorResponses;
        }

        private void ValidateEventTypes(string eventType, IList<ErrorResponse> errorResponses)
        {
            var eventTypes = _giftAidCalculators.Select(x => x.GetGiftAidType()).ToList();

            if (!eventTypes.Contains(eventType))
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.EventType,
                    $"Only supported event types are {String.Join(",", eventTypes)}"));
        }

        private async Task ValidateCountry(string countryCode, IList<ErrorResponse> errorResponses)
        {
            if (String.IsNullOrEmpty(countryCode))
            { 
                errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Country,
                    $"{GiftAidConstants.InputFields.Country} Cannot be Empty"));
            }
            else
            {
                var countryList = await _countryService.GetCountryByCountryCode(countryCode);

                if (countryList == null || !countryList.Any() || countryList.Single().CountryCode != countryCode)
                    errorResponses.Add(new ErrorResponse(GiftAidConstants.InputFields.Country,
                        $"{GiftAidConstants.InputFields.Country} is not a valid country code"));
            }
        }
    }
    
}