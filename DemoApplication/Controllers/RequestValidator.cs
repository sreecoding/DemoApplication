using System;
using System.Collections.Generic;

namespace DemoApplication.Controllers
{

    public interface IRequestValidator
    {
        List<ErrorResponse> Validate(decimal donationAmount, string country);
    }

    public class RequestValidator : IRequestValidator
    {

        public List<ErrorResponse> Validate(decimal donationAmount, string country)
        {
            IList<ErrorResponse> errorResponses = new List<ErrorResponse>();

            if (String.IsNullOrEmpty(country))
                AddErrorResponse(errorResponses, "country", "Country Cannot be Empty");

            if (donationAmount <= 0)
                AddErrorResponse(errorResponses, "donation", "Donation cannot be <= 0");

            return (List<ErrorResponse>) errorResponses;
        }

        private void AddErrorResponse(IList<ErrorResponse> errorResponses, string parameterName, string countryCannotBeEmpty)
        {
            errorResponses.Add(new ErrorResponse { ParameterName = parameterName, ErrorMessage = countryCannotBeEmpty });
        }
    }
    
}