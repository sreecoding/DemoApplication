namespace DemoApplication.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        Country GetCountryByCountryCode(string countryCode);
    }
}