using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{

    public partial class Siteswap
    {

        /// <summary>
        /// Applies a manipulation to the current siteswap.
        /// index value -1 (default) means this is a full pattern manipulation.
        ///
        /// If a manipulation is handled here, it will appear in the
        /// (throw or pattern level) manipulations.
        /// See also TestManipulationButton below, which handles others.
        /// </summary>
        /// <param name="manipulation"></param>
        /// <param name="throwIndex"></param>
        /// <returns></returns>
        public Siteswap Manipulate(Manipulation manipulation, int throwIndex = -1)
        {
            if (throwIndex == -1)
            {
                return manipulation switch
                {
                    Manipulation.AddCycle => AddCycle,
                    Manipulation.RemoveCycle => RemoveCycle,
                    Manipulation.SingleCycle => SingleCycleManipulation,
                    Manipulation.TimeReverse => TimeReverse,
                    Manipulation.Dual => Dual,
                    Manipulation.Showerify => Showerify,
                    Manipulation.DeShowerify => DeShowerify,
                    Manipulation.MakeOneHanded => MakeOneHanded,
                    Manipulation.MakeTwoHanded => MakeTwoHanded,
                    Manipulation.ExtendUp => ExtendUp,
                    Manipulation.ExtendDown => ExtendDown,
                    Manipulation.TruncateHighest => TruncateHighest,
                    Manipulation.TruncateLowest => TruncateLowest,
                    Manipulation.ShiftLeft => ShiftLeftManipulation,
                    Manipulation.ShiftRight => ShiftRightManipulation,
                    Manipulation.Shift => ShiftManipulation,
                    Manipulation.Reset => Reset,
                    _ => null,
                };
            }
            return manipulation switch
            {
                Manipulation.AddPeriod => AddPeriod(throwIndex),
                Manipulation.SubtractPeriod => SubstractPeriod(throwIndex),
                Manipulation.InsertAfter => InsertAfter(throwIndex),
                Manipulation.RotateToStart => RotateToStart(throwIndex),
                Manipulation.RemoveOrbit => RemoveOrbit(throwIndex),
                Manipulation.Delete => RemoveAt(throwIndex),
                _ => null,
            };
        }

        // Return result of manipulation to see if that button should show.
        // These manipulations don't have to be handled by Manipulate(manip, index)
        public Siteswap TestManipulationButton(Manipulation manipulation, int throwIndex = -1)
        {
            return manipulation switch
            {
                Manipulation.IncreaseAll => IncreaseAll,
                Manipulation.DecreaseAll => DecreaseAll,
                Manipulation.Insert => LastPossibleInsertion.Item2,
                _ => Manipulate(manipulation, throwIndex)
            };
        }

        private ManipulationResult ManipulationResult(Manipulation manipulation, int throwIndex = -1)
        {
            var result = Manipulate(manipulation, throwIndex);
            return result == null ? null : new ManipulationResult() {
                Manipulation = manipulation,
                DisplayName = manipulation.DisplayName(),
                Result = result
            };
        }

        /// <summary>
        /// Get list of possible manipulations for pattern or a single throw
        /// by doing every manipulation provided by Manipulate(manipulation, int).
        /// 
        /// All the algorithms try to return null if they fail validation,
        /// so here we get a list of the ones that returned something.
        /// </summary>
        /// <param name="throwIndex"></param>
        /// <returns></returns>
        public List<ManipulationResult> PossibleManipulations(int throwIndex = -1)
        {
            return Enum.GetValues(typeof(Manipulation))
                .Cast<Manipulation>()
                .Select(manip => ManipulationResult(manip, throwIndex))
                .OfType<ManipulationResult>() // strip out nulls
                .ToList();
        }

    }
}
