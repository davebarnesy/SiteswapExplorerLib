using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{

    /// <summary>
    /// Viewmodel for number cells.
    /// Doesn't handle updates - when siteswap changes, just generate a new list.
    /// </summary>
    ///
    public class ThrowCellViewModel: BaseCellViewModel
    {
        public readonly int ThrowIndex;
        public readonly int ThrowOrbit;
        public readonly List<ManipulationResult> Manipulations;

        public static ThrowCellViewModel CreateThrowCell(Siteswap siteswap, int throwIndex, int throwOrbit)
        {
            return new ThrowCellViewModel(siteswap, throwIndex, throwOrbit);
        }

        private ThrowCellViewModel(Siteswap siteswap, int throwIndex, int throwOrbit) : base(siteswap)
        {
            ThrowIndex = throwIndex;
            ThrowOrbit = throwOrbit;
            Manipulations = Siteswap.PossibleManipulations(throwIndex);
        }

        private ManipulationResult Check(Manipulation manipulation)
        {
            return Manipulations.FirstOrDefault(x => x.Manipulation == manipulation);
        }

        public string Text
        {
            get
            {
                return Siteswap.Throws[ThrowIndex].AsString; 
            }
        }

        public Siteswap AddPeriodResult
        {
            get => Check(Manipulation.AddPeriod)?.Result;
        }

        public Siteswap SubtractPeriodResult
        {
            get => Check(Manipulation.SubtractPeriod)?.Result;
        }

        public Siteswap DeleteResult
        {
            get => Check(Manipulation.Delete)?.Result;
        }

        public bool CanAddPeriod => AddPeriodResult != null;
        public bool CanSubtractPeriod => SubtractPeriodResult != null;
        public bool CanDelete => DeleteResult != null;

    }
}
