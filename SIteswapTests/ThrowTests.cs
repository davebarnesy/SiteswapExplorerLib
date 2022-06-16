using System.Linq;
using NUnit.Framework;
using SiteswapLib;

namespace SiteswapTests
{

    public class ThrowTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitGoodCharacter()
        {
            var c = Throw.Create('3');
            Assert.NotNull(c);
            Assert.AreEqual(c.Duration, 3);
        }

        [Test]
        public void InitBadCharacter()
        {
            var c = Throw.Create(':');
            Assert.IsNull(c);
        }

        [Test]
        public void InitWithDuration()
        {
            Assert.IsNull(Throw.Create(-1));
            Assert.IsNull(Throw.Create(Throw.allowedCharacters.Length));
            Assert.NotNull(Throw.Create(Throw.allowedCharacters.Length - 1));
            Assert.NotNull(Throw.Create(3));
        }

        [Test]
        public void ConvertToString()
        {
            var t = Throw.Create('a');
            Assert.AreEqual(t.AsString, "a");
        }

        [Test]
        public void IncreasedBy()
        {
            var t = Throw.Create('5');
            var result = t.IncreasedBy(3);
            Assert.AreEqual(result.AsString, "8");
        }

        [Test]
        public void DecreasedBy()
        {
            var t = Throw.Create('5');
            var result = t.DecreasedBy(3);
            Assert.AreEqual(result.AsString, "2");
        }

        [Test]
        public void DecreasedTooFar()
        {
            var t = Throw.Create('5');
            var result = t.DecreasedBy(6);
            Assert.IsNull(result);
        }

        [Test]
        public void IncreasedTooFar()
        {
            var t = Throw.Create(Throw.allowedCharacters.Last());
            var result = t.IncreasedBy(1);
            Assert.IsNull(result);
        }

    }
}
