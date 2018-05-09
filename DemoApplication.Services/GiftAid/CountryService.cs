using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Services.GiftAid
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Country GetCountryByCountryCode(string countryCode)
        {
            return _countryRepository.GetCountryByCountryCode(countryCode);
        }
    }

    public interface ICountryRepository
    {
        Country GetCountryByCountryCode(string countryCode);
    }

    public interface ICountryService
    {
        Country GetCountryByCountryCode(string countryCode);
    }

    public class Country
    {
        public Country(string countryCode, string countryName)
        {
            CountryCode = countryCode;
            CountryName = countryName;
        }

        public string CountryCode { get; }
        public string CountryName { get; }
    }
}
