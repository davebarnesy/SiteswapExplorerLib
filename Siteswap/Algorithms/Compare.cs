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
    }
}
