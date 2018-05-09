using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApplication.Repositories
{
    public interface ITaxRepository
    {
        Task<List<TaxData>> GetTaxRate(string countryCode);
    }
}