using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {
        public Siteswap Swap(int index, int by, bool shouldSort = true)
        {
            var targetIndex = (index + by + Period) % Period;
            if (targetIndex == index)
            {
                return null; // swap with self
            }
            var abs = Math.Abs(by);
            var change1 = by < 0 ? abs : -abs;
            var change2 = by < 0 ? -abs : abs;

            var firstModifiedThrow = Throw.Create(Throws[index].Duration + change1);
            var secondModifiedThrow = Throw.Create(Throws[targetIndex].Duration + change2);
            if (firstModifiedThrow == null || secondModifiedThrow == null)
            {
                return null;
            }

            var result = Duplicate;
            result.Throws.RemoveAt(index);
            result.Throws.Insert(index, secondModifiedThrow);
            result.Throws.RemoveAt(targetIndex);
            result.Throws.Insert(targetIndex, firstModifiedThrow);
            return shouldSort ? result.Validate : result.Sanitised;
        }

        public List<(int by, Siteswap result)> PossibleSwaps(int index)
        {
            var result = new List<(int by, Siteswap result)>();
            for (int by = -Period + 1; by < Period; by++)
            {
                if (by == 0) 
                {
                    continue; // can't swap with self
                }
                var swapResult = Swap(index, by);
                if (swapResult == null)
                {
                    continue;
                }
                result.Add((by, result: swapResult));
            }
            return result;
        }

        /// <summary>
        /// When doing a 'drag and swap', we want to swap two throws.
        /// But that can mean two swaps (one forward, one backward).
        /// i.e. is a swap of the ends of 55555 meant to be 95551 or 45556?
        /// That's why the boolean exists.
        /// </summary>
        /// <param name="throwIndex"></param>
        /// <param name="withIndex"></param>
        /// <returns></returns>
        public Siteswap DragSwap(int throwIndex, int withIndex, bool forwardFirst)
        {
            if (throwIndex == withIndex)
            {
                return null;
            }
            if (throwIndex < 0 || withIndex < 0 || throwIndex >= Period || withIndex >= Period)
            {
                return null;
            }
            return DragResult(throwIndex, withIndex, forwardFirst);
        }

        public List<Siteswap> DragSwapsForIndex(int index, bool forwardFirst)
        {
            return Throws.WithIndex().Select((_, i) => DragSwap(index, i, forwardFirst)).ToList();
        }

        public List<List<Siteswap>> DragSwapsForPattern(bool forwardFirst)
        {
            return Throws.WithIndex().Select((_, i) => DragSwapsForIndex(i, forwardFirst)).ToList();
        }

        /// <summary>
        /// These methods (swaptargets, swapforward, swapbackward) are part of the
        /// new Drag & Swap implementation: instead of trying to pick a 'right' swap,
        /// two lists are populated for the javascript. The main one is 'swapping forwards'
        /// and the other is 'swapping backwards'
        /// </summary>
        /// <param name="throwIndex"></param>
        /// <param name="withIndex"></param>
        /// <returns></returns>
        public (int forward, int backward) SwapTargets(int throwIndex, int withIndex)
        {
            var diff1 = withIndex - throwIndex;
            var diff2 = diff1 > 0 ? diff1 - Period : diff1 + Period;
            return (forward: Math.Max(diff1, diff2), backward: Math.Min(diff1, diff2));
        }

        public Siteswap SwapForward(int throwIndex, int withIndex)
        {
            return Swap(throwIndex, SwapTargets(throwIndex, withIndex).forward);
        }

        public Siteswap SwapBackward(int throwIndex, int withIndex)
        {
            return Swap(throwIndex, SwapTargets(throwIndex, withIndex).backward);
        }

        // Returns the swap in the stated direction, or the other one if null.
        public Siteswap DragResult(int throwIndex, int withIndex, bool forwardFirst)
        {
            var forward = SwapForward(throwIndex, withIndex);
            var backward = SwapBackward(throwIndex, withIndex);
            if (forwardFirst)
            {
                return forward ?? backward;
            }
            return backward ?? forward;
        }

    }
}
