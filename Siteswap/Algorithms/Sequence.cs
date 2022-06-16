using System;
namespace SiteswapLib
{
    partial class Siteswap
    {

        /// <summary>
        /// identify sequence steps. 0 means no sequence.
        /// Needs to be supplied with a sorted siteswap.
        /// Will be called twice, once sort asc, once sort desc.
        /// </summary>
        public int IdentifySequenceAfterSort
        {
            get {
                if (Period < 2)
                {
                    return 0;
                }
                var previousThrow = Throws[0].Duration;

                var doneFirst = false;

                var steps = 0;
                for (int i = 1; i < Period; i++)
                {
                    var thisThrow = Throws[i].Duration;
                    var thisStep = thisThrow - previousThrow;

                    if (thisStep == 0)
                    {
                        return 0; // two throws the same: not a sequence
                    }
                    else {
                        if (!doneFirst)
                        {
                            doneFirst = true;
                        }
                        else {
                            if (steps != thisStep)
                            {
                                return 0; // sequence broken
                            }
                        }
                        steps = thisStep;
                        previousThrow = thisThrow;
                    }
                }
                return steps;
            }
        }

        public int Steps
        {
            get
            {
                if (Throws.Count < 3 || Throws.AllSame())
                {
                    return 0; // can't be a sequence
                }
                var asc = SortLowestFirst.IdentifySequenceAfterSort;
                var desc = SortHighestFirst.IdentifySequenceAfterSort;

                // Might still return 0 if desc is also 0
                var steps = asc == 0 ? desc : asc;
                return steps;
            }
        }

        // 
        /// <summary>
        /// 534 should be 56734. Ascending sequence, so go 'steps' higher each step,
        /// and insert after highest value.
        /// 97531 should be db97531. Descending sequence. so go abs(steps) higher
        /// and insert before highest.
        /// </summary>
        public Siteswap ExtendUp
        {
            get
            {
                return Extend(up: true);
            }
        }

        public Siteswap ExtendDown
        {
            get
            {
                return Extend(up: false);
            }
        }

        public Siteswap TruncateHighest
        {
            get
            {
                return Truncate(high: true);
            }
        }

        public Siteswap TruncateLowest
        {
            get
            {
                return Truncate(high: false);
            }
        }

        private Siteswap Truncate(bool high)
        {
            if (Steps == 0 || Throws.Count < 3)
            {
                return null;
            }
            var result = Duplicate;
            result.RemoveThrow(high).RemoveThrow(high);
            return result.Sanitised;
        }

        /// <summary>
        /// Used for 'truncate' methods. Might return an invalid siteswap
        /// because the operation is a pair of removals. The "Truncate...' methods
        /// need to make sure they call Sanitised to make sure the result is valid.
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        private Siteswap RemoveThrow(bool high)
        {
            var duration = high ? Highest : Lowest;
            var index = Throws.FindIndex(x => x.Duration == duration);
            Throws.RemoveAt(index);
            return this;
        }

        /// <summary>
        /// Extend a sequence by adding two throws to either end
        /// "up=true" means 'add two higher throws'
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        private Siteswap Extend(bool up)
        {
            var steps = Steps;
            if (steps == 0)
            {
                return null; // there's no sequence
            }
            var end = up ? Highest : Lowest;
            var increment = Math.Abs(steps);
            var ascending = steps > 0;
            var new1 = Throw.Create(up ? end + increment : end - increment);
            var new2 = Throw.Create(up ? end + (increment * 2) : end - (increment * 2));
            var indexOfEnd = Throws.FindIndex(x => x.Duration == end);

            if (new1 == null || new2 == null)
            {
                // a new throw is out of range so there's no extend possible.
                return null;
            }
            // descending: db 975 31
            // ascending : 12 345 67

            // This used to allow sequence from siteswaps with period 2.
            // But that meant 53 for example would get treated as ascending by 2 when
            // it's also descending by 2. Fixed that by restricting to period 3.

            // 345 up. Up ascending. Highest index = 2. Insert at 3 then 4.
            // 345 down. Down ascending. Lowest index is 0. insert at 0 then 0.
            // 975 up. Up descending. Highest index is 0. Insert b at 0 then d 0.
            // 975 down. Down descending. Lowest index is 2. Insert 3 at 2 then 1 at 3. 

            // down asc and up desc both insert at indexOfEnd.

            var result = Duplicate;
            if ((ascending && !up) || (!ascending && up))
            {
                result.Throws.Insert(indexOfEnd, new1);
                result.Throws.Insert(indexOfEnd, new2);
            }
            else
            {
                result.Throws.Insert(indexOfEnd + 1, new1);
                result.Throws.Insert(indexOfEnd + 2, new2);
            }
            return result.Sanitised;
        }

    }
}
