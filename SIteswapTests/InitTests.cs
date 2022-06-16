using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class InitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitGoodSiteswap()
        {
            var ss = Siteswap.Create("534");
            Assert.NotNull(ss);
            Assert.AreEqual(ss.Period, 3);
            Assert.IsTrue(ss.IsValid);
        }

        [Test]
        public void NumberOfItems()
        {
            Assert.AreEqual(Siteswap.Create("534")?.NumberOfItems, 4);
            Assert.AreEqual(Siteswap.Create("123456789")?.NumberOfItems, 5);
            Assert.AreEqual(Siteswap.Create("745")?.NumberOfItems, null);
        }

        [Test]
        public void InitCollidingSiteswap()
        {
            var ss = Siteswap.Create("544");
            Assert.IsFalse(ss?.IsValid ?? false);
            Assert.IsNull(ss);
        }

        [Test]
        public void InitBadCharacterSiteswap()
        {
            var ss = Siteswap.Create("5$4");
            Assert.IsNull(ss);
        }
    }
}
