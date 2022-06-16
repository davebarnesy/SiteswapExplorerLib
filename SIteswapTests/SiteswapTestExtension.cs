using System;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{

    /// <summary>
    /// Run test and make sure input stays the same.
    /// </summary>
    public static class SiteswapTestExtension
    {
        public static void RunTest(this Siteswap input, Action<Siteswap> test)
        {
            var startSiteswapString = input.AsString;
            test.Invoke(input);
            Assert.AreEqual(input.AsString, startSiteswapString);
        }
    }

}
