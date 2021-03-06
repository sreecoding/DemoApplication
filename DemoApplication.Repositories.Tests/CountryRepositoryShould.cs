﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DemoApplication.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Repositories.Tests
{
    [TestFixture]
    public class CountryRepositoryShould
    {
        public string ConnectionString;
        private ICountryRepository _countryRepository;
        private const string CountryCode = "TS";
        private Mock<IConnectionStringConfig> _connectionStringConfig;

        [SetUp]
        public async Task Setup()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;
            _connectionStringConfig = new Mock<IConnectionStringConfig>();

            _connectionStringConfig.Setup(x => x.GetConnectionString())
                .Returns(ConnectionString);

            await SetupCountryDataInDatabase();
        }

        [Test]
        public async Task GivenValidCountryCode_ReturnsCorrectCountry()
        {
            _countryRepository = new CountryRepository(_connectionStringConfig.Object);

            var countryList = await _countryRepository.GetCountryByCountryCode(CountryCode);

            countryList.Single().CountryCode.ShouldBe("TS");
        }

        [Test]
        public async Task GivenInValidCountryCode_ReturnsEmptyList()
        {
            _countryRepository = new CountryRepository(_connectionStringConfig.Object);

            var countryList = await _countryRepository.GetCountryByCountryCode("ER");

            countryList.Count().ShouldBe(0);
        }

        [TearDown]
        public async Task TearDown()
        {
            await DeleteCountryDataFromDatabase();
        }

        private async Task SetupCountryDataInDatabase()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                await InsertCountryData(db, RepositoryTestConstants.InsertCountry);
            }
        }

        private async Task InsertCountryData(IDbConnection db, string insertStatement)
        {
            var countryCodeParameter = new DynamicParameters();
            countryCodeParameter.Add("@CountryCode", CountryCode);

            await db.ExecuteAsync(insertStatement, countryCodeParameter);
        }

        private async Task DeleteCountryDataFromDatabase()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                await DeleteTestData(db, RepositoryTestConstants.DeleteCountry);
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