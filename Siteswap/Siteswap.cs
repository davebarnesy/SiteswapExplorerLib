using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{

    public partial class Siteswap
    {

        public List<Throw> Throws;

        public static Siteswap Create(string value)
        {
            var throws = new List<Throw>();
            foreach (var c in value)
            {
                var t = Throw.Create(c);
                if (t == null)
                {
                    return null;
                }
                throws.Add(t);
            }

            // now construct a siteswap, then validate it as well
            var testSS = new Siteswap(throws);
            return testSS.IsValid ? testSS : null;
        }

        private Siteswap(List<Throw> throws)
        {
            Throws = throws;
        }

        /// <summary>
        /// Retuns new SS built from this one's AsString.
        /// Used to prevent changes to the current SS when making modifications.
        /// </summary>
        public Siteswap Duplicate
        {
            get => Create(AsString);
        }

        public static Siteswap Reset
        {
            get => Create(FourHanded ? "55" : "33");
        }

        public List<ThrowCellViewModel> CellViewModels
        {
            get
            {
                var orbits = Orbits;

                var result = new List<ThrowCellViewModel>();
                foreach (var (_, index) in Throws.WithIndex())
                {
                    result.Add(ThrowCellViewModel.CreateThrowCell(this, index, orbits[index]));
                }
                return result;
            }
        }

    }
}
