using System;
using System.Collections.Generic;

namespace SiteswapLib
{
    public partial class Siteswap
    {
        public Siteswap TimeReverse
        {
            get
            {
                var result = Duplicate;

                var newItems = new List<Throw>();
                var newIndexes = new List<int>();

                foreach (var (item, index) in Throws.WithIndex())
                {
                    var newIndex = (Period + index + item.Duration) % Period; // todo stop repeating this
                    newIndexes.Add(newIndex);
                    newItems.Add(Throw.Create(0));
                }

                foreach (var (item, index) in Throws.WithIndex())
                {
                    newItems[newIndexes[index]] = item;
                }
                newItems.Reverse();
                result.Throws = newItems;
                return IfDifferent(result.Sanitised);
            }
        }
    }
}
