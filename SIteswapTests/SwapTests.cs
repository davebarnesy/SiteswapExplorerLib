using System;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{
    public class SwapTests
    {
        [Test]
        public void Swap1()
        {
            Siteswap.Create("333").RunTest(x => {
                Assert.AreEqual("423", x.Swap(index: 0, by: 1).AsString);
            });
        }

        [Test]
        public void SwapMinus1()
        {
            Siteswap.Create("333").RunTest(x => {
                Assert.AreEqual("234", x.Swap(index: 0, by: -1).AsString);
            });
        }

        [Test]
        public void Swap2()
        {
            Siteswap.Create("333").RunTest(x => {
                Assert.AreEqual("531", x.Swap(index: 0, by: 2).AsString);
            });
        }

        [Test]
        public void PossibleSwaps()
        {
            Siteswap.Create("333").RunTest(x => {

                var text = "";
                foreach (var (by, result) in x.PossibleSwaps(0))
                {
                    text += result.AsString;
                };
                Assert.AreEqual("153234423531", text);
            });
        }

        [Test]
        public void DragSwapSame()
        {
            Siteswap.Create("333").RunTest(x =>
            {
                Assert.IsNull(x.DragSwap(0, 0, true));
            });
        }

        [Test]
        public void DragSwapOutOfRange()
        {
            Siteswap.Create("333").RunTest(x =>
            {
                Assert.IsNull(x.DragSwap(-1, 0, forwardFirst: true));
            });
            Siteswap.Create("333").RunTest(x =>
            {
                Assert.IsNull(x.DragSwap(0, 4, forwardFirst: true));
            });
        }

        // output we end up wanting for 333
        //[0, "423", "531"],
        //["153", 0, "342"],
        //["234", "315", 0]
        //
        // 633 is a counter example. The 6 always decreases.
        // maybe still need nicer automatic logic for these different cases
        //-2: 183
        //-1: 237
        //1: 453
        //2: 534
        [Test]
        public void DragSwapNormal()
        {
            Siteswap.Create("333").RunTest(x =>
            {
                Assert.AreEqual("153", x.DragSwap(1, 0, forwardFirst: true).AsString);
            });

            Siteswap.Create("333").RunTest(x =>
            {
                Assert.AreEqual("342", x.DragSwap(1, 2, forwardFirst: true).AsString);
            });
        }

        [Test]
        public void DragSwapsForIndex()
        {
            Siteswap.Create("55555").RunTest(x =>
            {
                Assert.AreEqual(x.Period, x.DragSwapsForIndex(2, forwardFirst: true).Count);
            });
        }


        [Test]
        public void DragSwapsForPattern()
        {
            Siteswap.Create("55555").RunTest(x =>
            {
                Assert.AreEqual(x.Period, x.DragSwapsForPattern(forwardFirst: true).Count);
            });
        }

        [Test]
        public void SwapTargets()
        {
            var ss = Siteswap.Create("333");
            var (forward, backward) = ss.SwapTargets(0, 1);
            Assert.AreEqual(1, forward);
            Assert.AreEqual(-2, backward);
        }

        [Test]
        public void SwapForward()
        {
            var ss = Siteswap.Create("333");
            Assert.AreEqual("423", ss.SwapForward(0, 1).AsString);
        }

        [Test]
        public void SwapBackward()
        {
            var ss = Siteswap.Create("333");
            Assert.AreEqual("153", ss.SwapBackward(0, 1).AsString);
        }

    }
}
