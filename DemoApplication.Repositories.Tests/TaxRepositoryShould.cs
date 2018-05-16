using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Repositories.Tests
{
    [TestFixture]
    public class TaxRepositoryShould
    {
        public string ConnectionString;

        private ITaxRepository _taxRepository;
        private const string CountryCode = "TS";

        [SetUp]
        public async Task Setup()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;

            await SetupTestDataInDatabase();
        }

        [Test]
        public async Task GivenACountryCode_ReturnsTaxRate()
        {
            _taxRepository = new TaxRepository();

            var taxRates = await _taxRepository.GetTaxRate(CountryCode);

            taxRates.Single().TaxRate.ShouldBe(15);
        }

        [TearDown]
        public async Task TearDown()
        {
            await DeleteTestDataFromDatabase();
        }

        private async Task SetupTestDataInDatabase()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                await InsertInitialData(db, RepositoryTestConstants.InsertCountry);
                await InsertInitialData(db, RepositoryTestConstants.InsertTaxRateForCountry);
            }
        }

        private async Task InsertInitialData(IDbConnection db, string insertStatement)
        {
            var countryCodeParameter = new DynamicParameters();
            countryCodeParameter.Add("@CountryCode", CountryCode);

            await db.ExecuteAsync(insertStatement, countryCodeParameter);
        }


        private async Task DeleteTestDataFromDatabase()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                await DeleteTestData(db, RepositoryTestConstants.DeleteCountry);
                await DeleteTestData(db, RepositoryTestConstants.DeleteCountryTaxRate);
            }
        }

        private async Task DeleteTestData(IDbConnection db, string deleteStatement)
        {
            var countryCodeParameter = new DynamicParameters();
            countryCodeParameter.Add("@CountryCode", CountryCode);

            await db.ExecuteAsync(deleteStatement, countryCodeParameter);
        }
    }
}
