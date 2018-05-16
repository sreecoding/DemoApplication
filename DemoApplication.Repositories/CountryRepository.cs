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
    public class CountryRepository : ICountryRepository
    {
        private readonly IConnectionStringConfig _connectionStringConfig;

        public CountryRepository(IConnectionStringConfig connectionStringConfig)
        {
            _connectionStringConfig = connectionStringConfig;
        }

        public async Task<List<Country>> GetCountryByCountryCode(string countryCode)
        {
            using (IDbConnection db = new SqlConnection(_connectionStringConfig.GetConnectionString()))
            {
                const string countryStoredProcedure = "GetCountryByCountryCode";

                var countryCodeParameter = new DynamicParameters();
                countryCodeParameter.Add("@CountryCode", countryCode);

                return (await db.QueryAsync<Country>(countryStoredProcedure, countryCodeParameter, commandType: CommandType.StoredProcedure)).ToList();
            }
        }
    }
}