using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DemoApplication.Repositories
{
    public interface ITaxRepository
    {
        Task<List<TaxData>> GetTaxRate(string uk);
    }

    public class TaxRepository : ITaxRepository
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;


        public Task<List<TaxData>> GetTaxRate(string uk)
        {
           using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string readSp = "GetAllTaxData";
                return Task.FromResult(db.Query<TaxData>(readSp, commandType: CommandType.StoredProcedure).ToList());
            }
        }
    }

    public class TaxData
    {
        public string Country;
        public decimal TaxRate;
    }
}