using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class ArithmeticTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IncreaseAll()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                var result = x.IncreaseAll;
                var expected = Siteswap.Create("645");
                Assert.AreEqual(result.AsString, expected.AsString);
            });

        }

        [Test]
        public void DecreaseAll()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                var result = x.DecreaseAll;
                var expected = Siteswap.Create("423");
                Assert.AreEqual(result.AsString, expected.AsString);
            });
        }

        [Test]
        public void DecreaseAllTooLow()
        {
            Siteswap.Create("504").RunTest(x =>
            {
                var result = x.DecreaseAll;
                Assert.IsNull(result);
            });
        }

        [Test]
        public void AddPeriod()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                var result = x.AddPeriod(1);
                var expected = Siteswap.Create("564");
                Assert.AreEqual(result.AsString, expected.AsString);
            });
        }

        [Test]
        public void SubtractPeriod()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                var result = x.SubstractPeriod(1);
                var expected = Siteswap.Create("504");
                Assert.AreEqual(result.AsString, expected.AsString);
            });
        }

        [Test]
        public void ChangePeriodOutOfRange()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.IsNull(x.SubstractPeriod(4));
            });
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.IsNull(x.SubstractPeriod(-4));
            });
        }

        [Test]
        public void SubtractPeriodTooFar()
        {
            Siteswap.Create("423").RunTest(x =>
            {
                var result = x.SubstractPeriod(1);
                Assert.IsNull(result);
            });

        }

    }
}
