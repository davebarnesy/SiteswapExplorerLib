using System;
namespace SiteswapLib
{
    public partial class Siteswap
    {
        public Siteswap Dual
        {
            get
            {
                var result = Duplicate;
                foreach (var t in result.Throws)
                {
                    var newDuration = (2 * NumberOfItems) - t.Duration;
                    var newThrow = Throw.Create(newDuration);
                    if (newThrow == null)
                    {
                        return null;
                    }
                    t.ReplaceDuration(newDuration);
                }
                result.Throws.Reverse();
                return IfDifferent(result.Sanitised);
            }
        }
    }
}
