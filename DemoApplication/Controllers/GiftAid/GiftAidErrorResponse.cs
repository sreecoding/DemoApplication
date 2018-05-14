using System.Collections.Generic;

namespace DemoApplication.Controllers.GiftAid
{
    public class GiftAidErrorResponse
    {
        public GiftAidErrorResponse(List<ErrorResponse> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IList<ErrorResponse> ValidationErrors { get; }
    }
}