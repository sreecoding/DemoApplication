using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DemoApplication.Repositories
{
    public class DatabaseCheck : IDatabaseCheck
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;

        public bool CheckConnection()
        {
            try
            {
                IDbConnection db = new SqlConnection(ConnectionString);
                db.Open();

                return true;
            }
            catch (DbException dataException)
            {
                Console.WriteLine(dataException);

                return false;
            }
        }
    }
}