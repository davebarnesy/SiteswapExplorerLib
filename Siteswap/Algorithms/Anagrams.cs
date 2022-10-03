using System;
using System.Collections.Generic;
using SiteswapLib.Extensions;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {

        /// <summary>
        /// Get list of anagrams for this siteswap.
        /// Autosort setting will determine if the list will contain rotations.
        ///
        /// Currently limited: only works for periods < 8 because the permutations
        /// calculations are not quick.
        /// </summary>
        /// <returns></returns>
        public List<Siteswap> Anagrams()
        {
            var workingList = new List<string>();
            if (Period > 7)
            {
                return new List<Siteswap> { this };
            }

            var inputAsString = AsString;
            var permutations = this.AsString.GetPermutations();

            // work out which permutations are not yet on the output list.
            // The call to Sanitised means rotations are not included when autosort is ON.
            foreach (var perm in permutations)
            {
                var ssString = Siteswap.Create(perm)?.Sanitised?.AsString;
                if (string.IsNullOrEmpty(ssString))
                {
                    continue;
                }

                if (workingList.Contains(ssString))
                {
                    continue;
                }
                workingList.Add(ssString);
            }
            workingList.Sort();
            return workingList.Select(x => Siteswap.Create(x)).ToList();
        }

    }
}

