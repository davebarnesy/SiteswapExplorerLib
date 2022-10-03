using NUnit.Framework;
using SiteswapLib;
using SiteswapLib.Extensions;
using System.Linq;

namespace SiteswapTests
{
    public class AnagramTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Permutations12345()
        {
            // Just wanted this one as a speed measure
            var input = "12345";
            var perms = input.GetPermutations();
            var list = input.ToCharArray().ToList();
        }

        [Test]
        public void AnagramsAutosortOn()
        {

            // when autosort is on, we don't want to see rotations in the anagram results.
            Siteswap.AutoSort = true;
            var ss = Siteswap.Create("12345");

            var anagrams = ss.Anagrams();
            Assert.AreEqual(3, anagrams.Count);

            // most tests assume autosort is oof, so set it back
            Siteswap.AutoSort = false;
        }

        [Test]
        public void AnagramsAutosortOff()
        {
            Siteswap.AutoSort = false;
            var ss = Siteswap.Create("12345");

            var anagrams = ss.Anagrams();
            Assert.AreEqual(15, anagrams.Count);
        }

    }
}