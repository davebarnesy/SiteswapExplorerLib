using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {

        private Dictionary<long, Siteswap> SortCandidates
        {
            get
            {
                var dict = new Dictionary<long, Siteswap>();
                var source = AsString + AsString;

                for (int i = 0; i < Throws.Count; i++)
                {
                    var sortItem = Create(source.Substring(i, Period));
                    var sortItemAsLong = sortItem.AsLong;
                    if (!dict.ContainsKey(sortItemAsLong))
                    {
                        dict.Add(sortItemAsLong, sortItem);
                    }
                }
                return dict;
            }
        }

        public Siteswap SortHighestFirst
        {
            get => Sort(highest: true);
        }

        public Siteswap SortLowestFirst
        {
            get => Sort(highest: false);
        }

        private Siteswap Sort(bool highest)
        {
            if (Throws.AllSame())
            {
                return this; // no sort necessary
            }
            var query = SortCandidates.Keys.OrderBy(x => x);
            var key = highest ? query.LastOrDefault() : query.FirstOrDefault();
            return SortCandidates[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="throwIndex"></param>
        /// <returns></returns>
        public Siteswap RotateToStart(int throwIndex, bool force = false)
        {
            // this is only for when autosort is off
            if (AutoSort || force)
            {
                return null;
            }

            // out of range check
            if (throwIndex < 0 || throwIndex >= Period)
            {
                return null;
            }

            // already at start
            if (throwIndex == 0)
            {
                return null;
            }
            var doubled = $"{AsString}{AsString}";
            var sub = doubled.Substring(throwIndex, Period);
            return Create(sub).Sanitised;

        }


    }
}
