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

        /// <summary>
        /// Shift Left but only give a result if it's different
        /// to keep the manipulations list as short as possible
        /// </summary>
        public Siteswap ShiftLeftManipulation
        {
            get
            {
                if (AutoSort)
                {
                    // there's no point rotating because autosort will cancel it out.
                    return null;
                }
                return IfNotSame(ShiftLeft);
            }
        }

        /// <summary>
        /// Shift Left but only give a result if it's different
        /// to keep the manipulations list as short as possible
        /// </summary>
        public Siteswap ShiftRightManipulation
        {
            get
            {
                if (AutoSort)
                {
                    // there's no point rotating because autosort will cancel it out.
                    return null;
                }
                return IfNotSame(ShiftRight);
            }
        }


        /// <summary>
        /// shift any siteswap one position to the left
        /// </summary>
        public Siteswap ShiftLeft
        {
            get
            {
                return ShiftOnePosition(forward: true);
            }
        }

        /// <summary>
        /// shift any siteswap one position to the right
        /// </summary>
        public Siteswap ShiftRight
        {
            get
            {
                return ShiftOnePosition(forward: false);
            }
        }

        /// <summary>
        /// 534 becomes: forward: 435, !forward: 345
        /// </summary>
        /// <param name="forward"></param>
        /// <returns></returns>
        private Siteswap ShiftOnePosition(bool forward)
        {
            if (Period < 2)
            {
                return null;
            }
            return RotateToStart(forward ? 1 : Period - 1, force: true);
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

            // todo: force check here is wrong

            // this manipulation is only for when autosort is off
            // or for when it's on but we've forced it to work
            if (AutoSort && !force)
            {
                return null;
            }

            // out of range check
            if (throwIndex < 0 || throwIndex >= Period)
            {
                return null;
            }

            // already at start
            if (throwIndex == 0 && !force)
            {
                return null;
            }
            var doubled = $"{AsString}{AsString}";
            var sub = doubled.Substring(throwIndex, Period);
            return Create(sub).Sanitised;

        }

    }
}
