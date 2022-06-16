using System;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {
        /// <summary>
        /// Returns a valid siteswap or null
        /// </summary>
        public Siteswap Validate
        {
            get => IsValid ? this : null;
        }

        /// <summary>
        /// Step through the siteswap. If valid, it will end up as all ones.
        /// </summary>
        public bool IsValid
        {
            get
            {
                var checkList = Enumerable.Repeat(1, Period).ToList();
                foreach (var (item, index) in Throws.WithIndex())
                {
                    var targetIndex = (index + item.Duration + Period) % Period;
                    checkList[targetIndex] = 0;
                }
                return !checkList.Contains(1);
            }
        }

        /// <summary>
        /// Most algorithms shoudl use this: makes sure resuilt is valid
        /// and that it is correctly sorted.
        /// </summary>
        public Siteswap Sanitised
        {
            get
            {
                if (!IsValid)
                {
                    return null;
                }
                return AutoSort ? SortHighestFirst : this;
            }
        }
    }
}
