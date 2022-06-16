using System;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{

    /// <summary>
    /// Every test should use the RunTest extension so it can also make sure
    /// that the input is not modified after the test.
    /// </summary>
    public class SiteswapTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsSameAs()
        {
            Siteswap.Create("1234567").RunTest(x =>
            {
                Assert.IsTrue(x.IsSameAs(Siteswap.Create("1234567")));
            });
        }

        [Test]
        public void IsEquivalentTo()
        {
            Siteswap.Create("1234567").RunTest(x =>
            {
                Assert.IsTrue(x.IsEquivalentTo(Siteswap.Create("1234567")));
            });
        }

        [Test]
        public void InsertAfter()
        {
            Siteswap.Create("5344").RunTest(x =>
            {
                Assert.IsNull(x.InsertAfter(0));
            });

            Siteswap.Create("5344").RunTest(x =>
            {
                Assert.AreEqual("53444", x.InsertAfter(1).AsString);
            });
        }

        [Test]
        public void RemoveAtGood()
        {
            // try valid removal. Make sure input SS stays same!
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.AreEqual(x.RemoveAt(2).AsString, "53");
            });
        }

        [Test]
        public void RemoveAtBad()
        {
            // removing the 3 would result in a non valid siteswap
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.IsNull(x.RemoveAt(1));
            });

        }

        [Test]
        public void RemoveLastRemainingDigit()
        {

            // Not allowed to delete last remaining digit
            Siteswap.Create("5").RunTest(x =>
            {
                Assert.IsNull(x.RemoveAt(0));
            });
        }

        [Test]
        public void PossibleInsertions()
        {
            Siteswap.Create("5344").RunTest(x =>
            {
                Assert.AreEqual(x.PossibleInsertions.Count, 3);
            });
        }

        [Test]
        public void LastPossibleInsertion()
        {
            Siteswap.Create("5344").RunTest(x =>
            {
                Assert.AreEqual(x.LastPossibleInsertion.Item2.AsString, "53444");
            });
        }

        [Test]
        public void Highest()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.AreEqual(x.Highest, 5);
            });
            Siteswap.Create("525").RunTest(x =>
            {
                Assert.AreEqual(x.Highest, 5);
            });
        }

        [Test]
        public void Lowest()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.AreEqual(x.Lowest, 3);
            });
        }

        [Test]
        public void Dual()
        {
            Siteswap.Create("633").RunTest(x =>
            {
                Assert.AreEqual("552", x.Dual.AsString);
            });
        }

        [Test]
        public void TimeReverse()
        {
            Siteswap.Create("51234").RunTest(x =>
            {
                Assert.AreEqual("24135", x.TimeReverse.AsString);
            });
        }

        [Test]
        public void Validate()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                Assert.IsTrue(x.IsValid);
            });

            // can't use runtest: should return null!
            Assert.IsNull(Siteswap.Create("544"));
        }

        [Test]
        public void ValidateWithZeroes()
        {
            Siteswap.Create("00005").RunTest(x =>
            {
                Assert.IsTrue(x.IsValid);
            });

        }


        [Test]
        public void Duplicate()
        {
            var input = "534";
            var ss = Siteswap.Create(input);

            Assert.IsTrue(ss.IsSameAs(ss.Duplicate));
        }

        [Test]
        public void PossibleManipulationsForPattern()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                var manipulations = x.PossibleManipulations();
                Assert.IsTrue(manipulations.Exists(m => m.Manipulation == Manipulation.Showerify));
                Assert.IsFalse(manipulations.Exists(m => m.Manipulation == Manipulation.AddPeriod));
            });
        }

        [Test]
        public void PossibleManipulationsForThrow()
        {
            Siteswap.Create("534").RunTest(x =>
            {
                var manipulations = x.PossibleManipulations(0);
                Assert.IsFalse(manipulations.Exists(m => m.Manipulation == Manipulation.Showerify));
                Assert.IsTrue(manipulations.Exists(m => m.Manipulation == Manipulation.AddPeriod));
            });
        }

        [Test]
        public void Orbits()
        {
            var orbits = Siteswap.Create("534").Orbits;
            Assert.AreEqual("010", string.Join("", orbits));

            var orbits2 = Siteswap.Create("504").Orbits;
            Assert.AreEqual("0-20", string.Join("", orbits2));

            var orbits3 = Siteswap.Create("633").Orbits;
            Assert.AreEqual("012", string.Join("", orbits3));

            var orbits4 = Siteswap.Create("633633").Orbits;
            Assert.AreEqual("012312", string.Join("", orbits4));
        }

        [Test]
        public void BallOrbits633633()
        {
            Siteswap.Create("633633").RunTest(x => {
                var ballorbits = x.BallOrbits(x.Orbits);
                Assert.AreEqual("0123", string.Join("", ballorbits.balls));
            });
        }
        [Test]
        public void BallOrbits534()
        {
            Siteswap.Create("534").RunTest(x => {
                var ballorbits = x.BallOrbits(x.Orbits);
                Assert.AreEqual("0100", string.Join("", ballorbits.balls));
            });
            // ss7 should return 7 items

            // also want to recieve throwslist? Might help colour held start balls in the .js


        }



        [Test]
        public void BallOrbitsAllSame()
        {
            Siteswap.Create("73").RunTest(x => {
                var ballorbits = x.BallOrbits(x.Orbits);
                Assert.AreEqual("00000", string.Join("", ballorbits.balls));
            });
        }

        [Test]
        public void AllSame()
        {
            Siteswap.Create("77777").RunTest(x => {
                Assert.IsTrue(x.Throws.AllSame());
            });
            Siteswap.Create("552").RunTest(x => {
                Assert.IsFalse(x.Throws.AllSame());
            });
        }

        [Test]
        public void RemoveOrbit()
        {
            Siteswap.Create("534").RunTest(x => {
                Assert.AreEqual("504", x.RemoveOrbit(1).AsString);
            });
        }


        // tuesday possibles
        // maximum period (if 0, don't limit)
        // session state in the mvc project
        // store each set ss in session state, use that if return home.
        // 
        // settings page:
        //   one switch for now: sort highest first

        // SingleCycle
        // AddCycle
        // RemoveCycle
        //
        // OrbitInfo(index): orbitID, withOrbit, withoutOrbit
        // HasMultipleOrbits

        // sequences:
        // SortLowFirst
        // SequenceSteps
        // ExtendUp
        // ExtendDown

        //Showerise
        // Showerise
        // DeShowerise
        // OneHanded
        // TwoHanded

        // Swaps
    }
}
