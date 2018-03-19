using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Controllers
{
    public class Donation
    {
        public Donation(decimal donationAmount, string country)
        {
            DonationAmount = donationAmount;
            Country = country;
        }

        [Required]
        [Range(0,100000)]
        public decimal DonationAmount { get; }

        [Required]
        [StringLength(2,MinimumLength = 2)]
        public string Country { get; }
    }
}