using System;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {
        public int Period
        {
            get => Throws.Count;
        }

        public int NumberOfItems
        {
            get => Throws.Sum(x => x.Duration) / Period;
        }

        public int Highest
        {
            get => Throws.OrderBy(x => x.Duration).LastOrDefault().Duration;
        }

        public int Lowest
        {
            get => Throws.OrderBy(x => x.Duration).FirstOrDefault().Duration;
        }

        private Siteswap IfDifferent(Siteswap newSiteswap)
        {
            return IsEquivalentTo(newSiteswap) ? null : newSiteswap;
        }
    }
}
