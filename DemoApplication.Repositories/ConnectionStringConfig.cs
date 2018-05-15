using System.Configuration;
using DemoApplication.Repositories.Interfaces;

namespace DemoApplication.Repositories
{
    public class ConnectionStringConfig : IConnectionStringConfig
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;
        }
    }
}