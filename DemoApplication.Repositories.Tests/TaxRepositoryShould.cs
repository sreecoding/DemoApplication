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

            await SetupTestDataInDatabase(CountryCode);
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
            await DeleteTestDataFromDatabase(CountryCode);
        }

        private async Task SetupTestDataInDatabase(string code)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

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


        private async Task DeleteTestDataFromDatabase(string code)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

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
