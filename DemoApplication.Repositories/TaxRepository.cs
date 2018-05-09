using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DemoApplication.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;

        public async Task<List<TaxData>> GetTaxRate(string countryCode)
        {
           using (IDbConnection db = new SqlConnection(ConnectionString))
           {
               const string taxRateStoredProcedure = "GetTaxRateByCountryCode";

               var countryCodeParameter = new DynamicParameters();
               countryCodeParameter.Add("@CountryCode", countryCode);

                return (await db.QueryAsync<TaxData>(taxRateStoredProcedure, countryCodeParameter, commandType: CommandType.StoredProcedure)) .ToList();
           }
        }
    }
}