using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApplication.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountryByCountryCode(string countryCode);
    }
}