using System;
using System.Linq;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class SortTests
    {

        [Test]
        public void SortHighFirst()
        {
            Siteswap.Create("19753").RunTest(x =>
            {
                Assert.AreEqual("97531", x.SortHighestFirst.AsString);
            });
        }

        [Test]
        public void SortLowFirst()
        {
            Siteswap.Create("51234").RunTest(x =>
            {
                Assert.AreEqual("12345", x.SortLowestFirst.AsString);
            });

        }

        [Test]
        public void RotateToStart()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.AreEqual("345", x.RotateToStart(1).AsString);
            });

        }

        [Test]
        public void ShiftLeft()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.AreEqual("345", x.ShiftLeft.AsString);
            });
        }

        [Test]
        public void ShiftRight()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.AreEqual("453", x.ShiftRight.AsString);
            });
        }

        [Test]
        public void ShiftRightPeriod1()
        {
            Siteswap.Create("7").RunTest(x =>
            {
                Assert.IsNull(x.ShiftRightManipulation);
            });
        }

        [Test]
        public void ShiftRightPeriod2()
        {
            // should return null. Period 2 means we want 'Shift'.
            Siteswap.Create("71").RunTest(x =>
            {
                Assert.IsNull(x.ShiftRightManipulation);
            });
        }

        [Test]
        public void Shift()
        {
            // should return null. Period 2 means we want 'Shift'.
            Siteswap.Create("71").RunTest(x =>
            {
                Assert.AreEqual("17", x.ShiftManipulation.AsString);
            });
        }


    }
}
