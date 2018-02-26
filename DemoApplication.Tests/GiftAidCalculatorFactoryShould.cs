using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Internal;


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
