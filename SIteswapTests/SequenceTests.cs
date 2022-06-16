using System;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class SequenceTests
    {

        [Test]
        public void DescendingSteps()
        {
            Assert.AreEqual(-2, Siteswap.Create("53197").Steps);

        }

        [Test]
        public void AscendingSteps()
        {
            Assert.AreEqual(1, Siteswap.Create("534").Steps);
        }

        [Test]
        public void StepsWhenNotSequence()
        {
            Assert.AreEqual(0, Siteswap.Create("552").Steps);
        }

        [Test]
        public void IdentifySequence()
        {
            Assert.AreEqual(1, Siteswap.Create("12345").IdentifySequenceAfterSort);
            Assert.AreEqual(-2, Siteswap.Create("97531").IdentifySequenceAfterSort);
        }

        [Test]
        public void ExtendUpAscending()
        {
            Siteswap.Create("345").RunTest(x =>
            {
                Assert.AreEqual("34567", x.ExtendUp.AsString);
            });
        }

        [Test]
        public void ExtendDownAscending()
        {
            Siteswap.Create("345").RunTest(x =>
            {
                Assert.AreEqual("12345", x.ExtendDown.AsString);
            });
        }

        [Test]
        public void ExtendDownDescending()
        {
            Siteswap.Create("975").RunTest(x =>
            {
                Assert.AreEqual("97531", x.ExtendDown.AsString);
            });
        }

        [Test]
        public void ExtendUpDescending()
        {
            Siteswap.Create("975").RunTest(x =>
            {
                Assert.AreEqual("db975", x.ExtendUp.AsString);
            });
        }

        [Test]
        public void TruncateHigh()
        {
            Siteswap.Create("97531").RunTest(x =>
            {
                Assert.AreEqual("531", x.TruncateHighest.AsString);
            });
        }


        [Test]
        public void TruncateLow()
        {
            Siteswap.Create("97531").RunTest(x =>
            {
                Assert.AreEqual("975", x.TruncateLowest.AsString);
            });
        }

    }
}
