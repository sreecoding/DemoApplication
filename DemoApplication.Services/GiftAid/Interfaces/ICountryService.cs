using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApplication.Repositories;

namespace DemoApplication.Services.GiftAid.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountryByCountryCode(string countryCode);
    }
}