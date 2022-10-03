using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib.Extensions
{

    /// <summary>
    /// Based on https://stackoverflow.com/a/38175625
    /// </summary>
    public static class StringExtension
    {
        static ICollection<string> result;

        public static ICollection<string> Permutations(this string str, int outputLength = -1)
        {
            if (outputLength == -1)
            {
                outputLength = str.Length;
            }
            result = new List<string>();
            MakePermutations(str.ToCharArray(), string.Empty, outputLength);
            return result;
        }

        private static void MakePermutations(
           char[] possibleArray,//all chars extracted from input
           string permutation,
           int outputLength) //the length of output
        {
            if (permutation.Length < outputLength)
            {
                for (int i = 0; i < possibleArray.Length; i++)
                {
                    var tempList = possibleArray.ToList<char>();
                    tempList.RemoveAt(i);
                    MakePermutations(tempList.ToArray(),
                         string.Concat(permutation, possibleArray[i]), outputLength);
                }
            }
            else if (!result.Contains(permutation))
                result.Add(permutation);
        }
    }
}

