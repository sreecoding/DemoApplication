using DemoApplication.Repositories;

namespace DemoApplication.Services.GiftAid.Interfaces
{
    public interface ICountryService
    {
        Country GetCountryByCountryCode(string countryCode);
    }
}