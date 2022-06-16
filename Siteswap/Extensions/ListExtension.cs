using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{


    public static class ListExtension
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static List<T> OddIndexes<T>(this List<T> source)
        {
            return source.Where((item, index) => index.IsOdd()).ToList();
        }

        public static List<T> EvenIndexes<T>(this List<T> source)
        {
            return source.Where((item, index) => index.IsEven()).ToList();
        }

        /// <summary>
        /// Check if all items in this sublist mtch the supplied duration
        /// to detect possible showered or one handed patterns.
        /// If this returns a match we'll know to continue with trying
        /// to halve the other throws.
        /// </summary>
        /// <param name="throws"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static bool AllEqualTo(this List<Throw> throws, int duration)
        {
            return throws.All(x => x.Duration == duration);
        }

        public static bool AllSame(this List<Throw> throws)
        {
            if (throws.Count < 2)
            {
                return true;
            }
            return throws.AllEqualTo(throws[0].Duration);
        }
    }
}
