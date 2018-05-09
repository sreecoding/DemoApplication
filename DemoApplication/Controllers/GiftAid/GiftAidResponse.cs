using System.Collections.Generic;

namespace DemoApplication.Controllers.GiftAid
{
    /// <summary>
    /// GiftAid
    /// </summary>
    public class GiftAidResponse
    {
        public GiftAidResponse(decimal giftAidAmount)
        {
            GiftAidAmount = giftAidAmount;
        }

        /// <summary>
        /// GiftAid Amount
        /// </summary>
        public decimal GiftAidAmount { get; }
        
    }
}