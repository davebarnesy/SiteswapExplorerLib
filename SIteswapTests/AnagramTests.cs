using NUnit.Framework;
using SiteswapLib;
using SiteswapLib.Extensions;

namespace SiteswapTests
{
    public class AnagramTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UnvalidatedPermutations()
        {
            // just a quick check on the string extension
            var text = "534";
            var permutations = text.Permutations();
            Assert.AreEqual(6, permutations.Count);
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