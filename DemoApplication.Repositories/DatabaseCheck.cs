using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DemoApplication.Repositories.Interfaces;

namespace DemoApplication.Repositories
{
    public class DatabaseCheck : IDatabaseCheck
    {
        private readonly IConnectionStringConfig _connectionStringConfig;

        public DatabaseCheck(IConnectionStringConfig connectionStringConfig)
        {
            this._connectionStringConfig = connectionStringConfig;
        }


        public bool CheckConnection()
        {
            try
            {
                IDbConnection db = new SqlConnection(_connectionStringConfig.GetConnectionString());
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