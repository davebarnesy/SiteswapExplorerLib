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
        /// </summary>
        /// <param name="includeSelf"></param>
        /// <returns></returns>
        public List<Siteswap> Anagrams()
        {

            var inputAsString = AsString;

            var workingList = new List<string>();

            // get all permutations of the string
            var permutations = this.AsString.Permutations();

            // work out which permutations are not yet on the output list.
            // The call to Sanitised means rotations are not included when autosort is ON.
            foreach (var perm in permutations)
            {
                var ssString = Siteswap.Create(perm)?.Sanitised?.AsString;
                if (!string.IsNullOrEmpty(ssString) && !workingList.Contains(ssString))
                {
                    workingList.Add(ssString);
                }
            }
            workingList.Sort();
            return workingList.Select(x => Siteswap.Create(x)).ToList();
        }

    }
}

