using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {

        public Siteswap Showerify
        {
            get
            {
                // If alternating throws are ones, it's probably already a shower
                if (Throws.OddIndexes().AllEqualTo(duration: 1) || Throws.EvenIndexes().AllEqualTo(duration: 1))
                {
                    return null;
                }
                return DoubleThrows(shower: true);
            }

        }

        public Siteswap MakeOneHanded
        {
            get
            {
                // If alternating throws are zeroes, it's already one handed
                if (Throws.OddIndexes().AllEqualTo(duration: 0) || Throws.EvenIndexes().AllEqualTo(duration: 0))
                {
                    return null;
                }
                return DoubleThrows(shower: false);
            }
        }

        public Siteswap DeShowerify
        {
            get
            {
                return RemoveAlternatingPatternAndHalve(shower: true);
            }
        }

        public Siteswap MakeTwoHanded
        {
            get
            {
                return RemoveAlternatingPatternAndHalve(shower: false);

            }
        }

        /// <summary>
        /// For producing one handed or shower version of pattern.
        /// The 'shower' boolean determines if we insert 0s or 1s in between.
        /// </summary>
        /// <param name="shower"></param>
        /// <returns></returns>
        private Siteswap DoubleThrows(bool shower)
        {
            var result = Duplicate;
            var between = shower ? 1 : 0;
            var newThrows = new List<Throw>();

            foreach (var t in Duplicate.Throws)
            {
                var newThrow = Throw.Create((t.Duration * 2) - between);
                if (newThrow == null)
                {
                    return null;
                }
                var newBetween = Throw.Create(between);
                newThrows.Add(newThrow);
                newThrows.Add(newBetween);
            }
            result.Throws = newThrows;
            return result.Sanitised;
        }

        /// <summary>
        /// Handles deshowerify and 'make two handed'
        /// by detecting alternating pattern and attempting to halve the other throws.
        /// shower=true means look for 1s and add 1 before halving
        /// shower=false means look for 0s then just halve
        /// </summary>
        /// <param name="shower"></param>
        /// <returns></returns>
        private Siteswap RemoveAlternatingPatternAndHalve(bool shower)
        {
            if (Period.IsOdd())
            {
                // can't be a shower if odd period
                return null;
            }

            var offset = shower ? 1 : 0;
            var throwsToHalve = new List<Throw>();

            if (Throws.OddIndexes().AllEqualTo(duration: offset))
            {
                throwsToHalve = Throws.EvenIndexes();
            }
            else
            {
                if (Throws.EvenIndexes().AllEqualTo(duration: offset))
                {
                    throwsToHalve = Throws.OddIndexes();
                }
            }

            if (!throwsToHalve.Any())
            {
                return null;
            }

            var output = new List<Throw>();
            foreach (var t in throwsToHalve)
            {
                var duration = (t.Duration + offset) / 2;
                var newThrow = Throw.Create(duration);
                if (newThrow == null)
                {
                    return null;
                }
                output.Add(newThrow);
            }

            var s = Duplicate;
            s.Throws = output;
            return s.Sanitised;
        }

    }
}