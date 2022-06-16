using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class ConvertTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertBackToString()
        {
            var ss = Siteswap.Create("534");
            Assert.AreEqual(ss.AsString, "534");
        }

        [Test]
        public void AsInt()
        {
            Assert.AreEqual(Siteswap.Create("534").AsLong, 6592);
            Assert.AreEqual(Siteswap.Create("345").AsLong, 4037);
            Assert.AreEqual(Siteswap.Create("55244").AsLong, 8634100);
            Assert.AreEqual(Siteswap.Create("52445").AsLong, 8496725);
            Assert.AreEqual(Siteswap.Create("7123456").AsLong, 15301447098);

        }
    }
}
