using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteswapLib
{
    public partial class Siteswap
    {

        public Siteswap RemoveOrbit(int index)
        {

            // todo - look into not offering this when the orbit is zeroes
            var orbits = Orbits;
            var result = Duplicate;

            if (orbits.Distinct().Count() < 2)
            {
                // only one orbit, don't offer to remove it.
                return null;
            }

            foreach (var (t, i) in result.Throws.WithIndex())
            {
                if (orbits[i] == orbits[index])
                {
                    t.ReplaceDuration(0);
                }
            }
            return result.Sanitised;
        }

        public List<int> Orbits
        {
            get {

                var result = Enumerable.Repeat(-1, Period).ToList();
                var orbitIndex = -1;

                do {
                    var startIndex = result.IndexOf(-1); // find next orbit to calculate
                    if (startIndex == -1) 
                    {
                        continue; // we're done if there are no not done orbits
                    }

                    var startThrow = Throws[startIndex];
                    if (startThrow.Duration == 0)
                    {
                        result[startIndex] = -2; // mark zero as done, get out of loop.
                        continue;
                    }

                    orbitIndex += 1;
                    result[startIndex] = orbitIndex;

                    var finishedOrbit = false;
                    var nextIndex = (startIndex + Throws[startIndex].Duration) % Period;

                    do
                    {
                        if (result[nextIndex] > -1)
                        {
                            finishedOrbit = true;
                            continue;
                        }
                        result[nextIndex] = orbitIndex;
                        nextIndex = (nextIndex + Throws[nextIndex].Duration) % Period;

                    } while (!finishedOrbit);

                } while (result.Contains(-1));
                return result;
            }

        }

        /// <summary>
        /// returns orbit ID for each ball.
        /// so 633633 is orbits 012312 and ballorbits 0123
        /// 'balls' is the orbit ID for each new ball to be thrown.
        /// 'throws' shows how that was calculated, but th efunction exits
        /// as soon as the ball orbits are known. But it will
        /// hopefully be useful enough for the javascript's held balls.
        /// </summary>
        /// <param name="orbits"></param>
        /// <returns></returns>
        public (List<int> balls, List<int> throws) BallOrbits(List<int> orbits)
        {

            // if orbits all same, just output that orbit ID for that many objects.
            var throwsList = Enumerable.Repeat(-1, 100).ToList();
            var result = new List<int>();
            do
            {
                var throwIndex = throwsList.IndexOf(-1);
                if (throwIndex == -1)
                {
                    // no throws to handle!
                    continue;
                }
                var ssIndex = throwIndex % Period;
                var thisOrbit = orbits[ssIndex];

                if (thisOrbit == -2)
                {
                    // it's a 0, mark that through the output list.
                    do
                    {
                        throwsList[throwIndex] = -2;
                        throwIndex += Period;
                    } while (throwIndex < throwsList.Count);
                    continue;
                }

                // we need to mark this orbit
                result.Add(thisOrbit);
                do {
                    throwsList[throwIndex] = thisOrbit;
                    var thisSkip = Throws[ssIndex].Duration;
                    throwIndex += thisSkip;
                    ssIndex = (ssIndex + thisSkip) % Period;
                } while (throwIndex < throwsList.Count && result.Count < NumberOfItems);

            } while (throwsList.Contains(-1) && result.Count < NumberOfItems);

            return (balls: result, throws: throwsList);
        }

    }

}
