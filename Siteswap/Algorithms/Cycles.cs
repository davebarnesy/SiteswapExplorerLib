using System;
using System.Linq;

namespace SiteswapLib
{

    public partial class Siteswap
    {
        // from https://www.geeksforgeeks.org/find-given-string-can-represented-substring-iterating-substring-n-times/
        // A utility function to fill lps[] or
        // compute prefix function used in KMP
        // string matching algorithm. Refer
        // https://www.geeksforgeeks.org/archives/11902
        // for details
        static void computeLPSArray(String str, int M, int[] lps)
        {

            // length of the previous
            // longest prefix suffix
            int len = 0;

            int i;

            lps[0] = 0; // lps[0] is always 0
            i = 1;

            // the loop calculates lps[i]
            // for i = 1 to M-1
            while (i < M)
            {
                if (str[i] == str[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // (pat[i] != pat[len])
                {
                    if (len != 0)
                    {

                        // This is tricky. Consider the
                        // example AAACAAAA and i = 7.
                        len = lps[len - 1];

                        // Also, note that we do
                        // not increment i here
                    }
                    else // if (len == 0)
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }

        public Siteswap Cycle
        {
            get {
                // Find length of string and create
                // an array to store lps values used
                // in KMP

                var str = AsString;

                int n = str.Length;
                int[] lps = new int[n];

                // Preprocess the pattern (calculate
                // lps[] array)
                computeLPSArray(str, n, lps);

                // Find length of longest suffix
                // which is also prefix of str.
                int len = lps[n - 1];

                // If there exist a suffix which is also
                // prefix AND Length of the remaining
                // substring divides total length, then
                // str[0..n-len-1] is the substring that
                // repeats n/(n-len) times (Readers can
                // print substring and value of n/(n-len)
                // for more clarity.
                var cycleString = (len > 0 && n % (n - len) == 0) ? str.Substring(0, n - len) : str;
                return Create(cycleString);
            }

        }

        public int CycleCount
        {
            get
            {
                return Period / Cycle.Period;
            }
        }

        public Siteswap AddCycle
        {
            get
            {
                return Create(AsString + Cycle.AsString);
            }
        }

        public Siteswap RemoveCycle
        {
            get
            {
                if (CycleCount < 2)
                {
                    return null;
                }
                var newString = string.Concat(Enumerable.Repeat(Cycle.AsString, CycleCount-1));
                return Create(newString);
            }
        }

        public Siteswap SingleCycleManipulation
        {
            get
            {
                // this is so, eg, 5353 gets remove cycle but not also single cycle
                if (CycleCount < 3)
                {
                    return null;
                }
                return Cycle;
            }
        }

    }

}
