
using System;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {

        public override string ToString()
        {
            return AsString;
        }

        public string AsString
        {
            get
            {
                return string.Join("", Throws.Select(x => x.AsString));
            }
        }

        // todo this is flawed: limited to int64.maxValue, which breaks sorting.
        public long AsLong
        {
            get
            {
                long multiplier = 1;
                long total = 0;
                var numberBase = 36; // max height should never go past z

                for (int i = Throws.Count - 1; i >= 0; i--)
                {
                    var addition = multiplier * Throws[i].Duration;
                    total += addition;
                    multiplier *= numberBase;
                }
                return total;
            }
        }

    }
}
