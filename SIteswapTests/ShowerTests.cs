using System;
using System.Linq;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class ShowerTests
    {

        [Test]
        public void Showerify()
        {
            Siteswap.Create("333").RunTest(x => {
                Assert.AreEqual("515151", x.Showerify.AsString);
            });
        }

        [Test]
        public void DeShowerify()
        {
            Siteswap.Create("515151").RunTest(x => {
                Assert.AreEqual("333", x.DeShowerify.AsString);
            });
        }

        [Test]
        public void MakeOneHanded()
        {
            Siteswap.Create("333").RunTest(x => {
                Assert.AreEqual("606060", x.MakeOneHanded.AsString);
            });
        }

        [Test]
        public void MakeTwoHanded()
        {
            Siteswap.Create("606060").RunTest(x => {
                Assert.AreEqual("333", x.MakeTwoHanded.AsString);
            });
            Siteswap.Create("0808").RunTest(x => {
                Assert.AreEqual("44", x.MakeTwoHanded.AsString);
            });
        }

        [Test]
        public void CheckAllSameTrue()
        {
            var s = Siteswap.Create("111");
            var result = s.Throws.AllEqualTo(1);
            Assert.IsTrue(result);

        }

        [Test]
        public void CheckAllSameFalse()
        {

            var s = Siteswap.Create("111");
            s.Throws[2] = Throw.Create(0); // nobbled to force the test case

            var result = s.Throws.AllEqualTo(1);
            Assert.IsFalse(result);
        }

        // this was giving bad response - make sure it gets out early now.
        [Test]
        public void DeshowerifyZeroes()
        {
            Siteswap.Create("600000").RunTest(x => {
                Assert.IsNull(x.DeShowerify);
            });

        }

    }
}
