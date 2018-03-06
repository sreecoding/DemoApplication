using DemoApplication.Services;
using NUnit.Framework;


namespace DemoApplication.Tests
{
    [TestFixture]
    public class GiftAidCalculatorFactoryShould
    {
        IGiftAidCalculatorFactory _serviceUnderTest;

        [SetUp]
        public void Setup()
        {

            _serviceUnderTest = new GiftAidCalculatorFactory();
        }

    [Test]
        public void ReturnServiceOfTypeCalculator()
        {
            var calculator = _serviceUnderTest.Generate();

            Assert.IsInstanceOf<IGiftAidCalculator>(calculator);

        }
    }
}
