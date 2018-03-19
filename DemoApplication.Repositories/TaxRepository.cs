using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DemoApplication.Repositories
{
    public interface ITaxRepository
    {
        IEnumerable<TaxData> GetTaxRate(string uk);
    }

    public class TaxRepository : ITaxRepository
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;


        public IEnumerable<TaxData> GetTaxRate(string uk)
        {
           using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string readSp = "GetAllTaxData";
                return db.Query<TaxData>(readSp, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }

    public class TaxData
    {
        public string Country;
        public decimal TaxRate;
    }
}