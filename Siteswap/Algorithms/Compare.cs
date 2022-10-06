using System;
namespace SiteswapLib
{
    public partial class Siteswap
    {
        /// <summary>
        /// Check both siteswaps are the same with current sort order
        /// </summary>
        public bool IsSameAs(Siteswap otherSiteswap)
        {
            return AsString.Equals(otherSiteswap.AsString);
        }

        /// <summary>
        /// Check both end up the same when sorted biggest first.
        /// </summary>
        public bool IsEquivalentTo(Siteswap otherSiteswap)
        {
            var ss1 = SortHighestFirst;
            var ss2 = otherSiteswap.SortHighestFirst;
            return ss1.IsSameAs(ss2);
        }

        /// <summary>
        /// Check if newSiteswap would be equivalent to the current instance
        /// </summary>
        /// <param name="newSiteswap"></param>
        /// <returns></returns>
        private Siteswap IfNotEquivalent(Siteswap newSiteswap)
        {
            return IsEquivalentTo(newSiteswap) ? null : newSiteswap;
        }

        /// <summary>
        /// Check if newSiteswap would be equivalent to the current instance
        /// </summary>
        /// <param name="newSiteswap"></param>
        /// <returns></returns>
        private Siteswap IfNotSame(Siteswap newSiteswap)
        {
            return IsSameAs(newSiteswap) ? null : newSiteswap;
        }
    }
}
