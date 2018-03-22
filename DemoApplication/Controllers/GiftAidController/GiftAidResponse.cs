using System;
using System.Collections.Generic;

namespace DemoApplication.Controllers.GiftAidController
{
    /// <summary>
    /// GiftAid
    /// </summary>
    public class GiftAidResponse
    {
        public GiftAidResponse(decimal giftAidAmount, IList<ErrorResponse> validationErrors)
        {
            GiftAidAmount = giftAidAmount;
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// GiftAid Amount
        /// </summary>
        public decimal GiftAidAmount { get; }

        /// <summary>
        /// Input Validation Errors
        /// </summary>
        public IList<ErrorResponse> ValidationErrors { get; }
    }
}