using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {
        public Siteswap InsertAfter(int index)
        {
            // force in a new throw at index. The result may be invalid.
            // That doesn't matter because the call to sanitised will validate it.
            var newThrow = Throw.Create(NumberOfItems);
            if (newThrow == null)
            {
                return null;
            }
            var dupe = Duplicate;
            dupe.Throws.Insert(index + 1, newThrow);
            return dupe.Sanitised;
        }

        public Siteswap RemoveAt(int index)
        {
            if (index + 1 > Period || Period == 1) return null;
            var dupe = Duplicate;
            dupe.Throws.RemoveAt(index);
            return dupe.Sanitised;
        }

        public List<(int index, Siteswap result)> PossibleInsertions
        {
            get
            {
                var result = new List<(int index, Siteswap siteswap)>();
                for (int i = 0; i < Throws.Count; i++)
                {
                    var insertion = Duplicate.InsertAfter(i);
                    if (insertion != null)
                    {
                        result.Add((i, insertion));
                    }
                }
                return result;
            }
        }

        public (int, Siteswap) LastPossibleInsertion
        {
            get => PossibleInsertions.LastOrDefault();
        }
    }
}
