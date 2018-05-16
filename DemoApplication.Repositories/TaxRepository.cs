using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DemoApplication.Repositories.Interfaces;

namespace DemoApplication.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly IConnectionStringConfig _connectionStringConfig;

        public TaxRepository(IConnectionStringConfig connectionStringConfig)
        {
            _connectionStringConfig = connectionStringConfig;
        }

        public async Task<List<TaxData>> GetTaxRate(string countryCode)
        {
           using (IDbConnection db = new SqlConnection(_connectionStringConfig.GetConnectionString()))
           {
               const string taxRateStoredProcedure = "GetTaxRateByCountryCode";

               var countryCodeParameter = new DynamicParameters();
               countryCodeParameter.Add("@CountryCode", countryCode);

                return (await db.QueryAsync<TaxData>(taxRateStoredProcedure, countryCodeParameter, commandType: CommandType.StoredProcedure)) .ToList();
           }
        }
    }
}