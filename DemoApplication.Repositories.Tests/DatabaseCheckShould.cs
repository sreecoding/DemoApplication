using System;
using System.Configuration;
using System.Threading.Tasks;
using DemoApplication.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace DemoApplication.Repositories.Tests
{
    [TestFixture]
    public class DatabaseCheckShould
    {
        public const string WrongConnection ="Server=tcp:zicolearning.database.windows.net,1433;Initial Catalog=DemoWrongDB;Persist Security Info=False;User ID=zicoadmin;Password=zicopassword1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public string ConnectionString;
        private IDatabaseCheck _databaseCheck;
        private Mock<IConnectionStringConfig> _connectionStringConfig;
        

        [SetUp]
        public void Setup()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;
            _connectionStringConfig = new Mock<IConnectionStringConfig>();
        }

        [Test]
        public void GivenCorrectConnectionString_ReturnsTrue()
        {
            _connectionStringConfig.Setup(x => x.GetConnectionString())
                .Returns(ConnectionString);

            _databaseCheck = new DatabaseCheck(_connectionStringConfig.Object);

            var isDatabaseHealthy = _databaseCheck.CheckConnection();
            
            isDatabaseHealthy.ShouldBe(true);
        }

        [Test]
        public void GivenWrongConnectionString_ReturnsFalse()
        {
            _connectionStringConfig.Setup(x => x.GetConnectionString())
                .Returns(WrongConnection);

            _databaseCheck = new DatabaseCheck(_connectionStringConfig.Object);

            var isDatabaseHealthy = _databaseCheck.CheckConnection();

            isDatabaseHealthy.ShouldBe(false);
        }

    }
}