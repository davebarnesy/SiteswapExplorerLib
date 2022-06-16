using System;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class CycleTests
    {

        [Test]
        public void Cycle()
        {
            Assert.AreEqual("53", Siteswap.Create("535353").Cycle.AsString);
            Assert.AreEqual("534", Siteswap.Create("534534").Cycle.AsString);
            Assert.AreEqual("97531", Siteswap.Create("97531").Cycle.AsString);
            Assert.AreEqual("5", Siteswap.Create("55555").Cycle.AsString);
            Assert.AreEqual("552", Siteswap.Create("552").Cycle.AsString);
        }

        [Test]
        public void CycleCount()
        {
            Assert.AreEqual(3, Siteswap.Create("535353").CycleCount);
            Assert.AreEqual(2, Siteswap.Create("534534").CycleCount);
            Assert.AreEqual(1, Siteswap.Create("97531").CycleCount);
            Assert.AreEqual(1, Siteswap.Create("53").CycleCount);
        }

        [Test]
        public void AddCycle()
        {
            Assert.AreEqual("53535353", Siteswap.Create("535353").AddCycle.AsString);
            Assert.AreEqual("9753197531", Siteswap.Create("97531").AddCycle.AsString);
        }

        [Test]
        public void RemoveCycle()
        {
            Assert.AreEqual("5353", Siteswap.Create("535353").RemoveCycle.AsString);
            Assert.IsNull(Siteswap.Create("53").RemoveCycle);
        }

        [Test]
        public void SingleCycleManipulation()
        {
            // if 1 cycle, do nothing
            // if 2 cycles, do nothing. Remove cycle should work.
            // if >2 cycles, return a single cycle
            Assert.AreEqual("53", Siteswap.Create("535353").SingleCycleManipulation.AsString);
            Assert.IsNull(Siteswap.Create("5353").SingleCycleManipulation);
        }

    }
}
