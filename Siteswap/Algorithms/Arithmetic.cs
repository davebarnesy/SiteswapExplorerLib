using System.Collections.Generic;

namespace SiteswapLib
{
    public partial class Siteswap
    {

        public Siteswap IncreaseAll
        {
            get
            {
                var newThrows = new List<Throw>();
                foreach (var t in Throws)
                {
                    var newt = t.IncreasedBy(1);
                    if (newt == null)
                    {
                        return null;
                    }
                    newThrows.Add(newt);
                }
                return new Siteswap(newThrows).Sanitised;
            }
        }

        public Siteswap DecreaseAll
        {
            get
            {
                var newThrows = new List<Throw>();
                foreach (var t in Throws)
                {
                    var newt = t.DecreasedBy(1);
                    if (newt == null)
                    {
                        return null;
                    }
                    newThrows.Add(newt);
                }
                return new Siteswap(newThrows).Sanitised;
            }
        }

        public Siteswap AddPeriod(int index)
        {
            if (index > Period || index < 0)
            {
                return null;
            }


            var newThrow = Throw.Create(Throws[index].Duration + Period);
            if (newThrow == null)
            {
                return null;
            }
            var dupe = Duplicate;
            dupe.Throws[index] = newThrow;
            return dupe.Sanitised;
        }

        public Siteswap SubstractPeriod(int index)
        {
            if (index > Period || index < 0)
            {
                return null;
            }
            var newThrow = Throw.Create(Throws[index].Duration - Period);
            if (newThrow == null)
            {
                return null;
            }
            var dupe = Duplicate;
            dupe.Throws[index] = newThrow;
            return dupe.Sanitised;
        }
    }
}
