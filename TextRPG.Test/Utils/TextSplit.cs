using NUnit.Framework;
using TextRPG.Utils;

namespace TextRPG.Test.Utils
{
    [TestFixture]
    public class TextSplitTest
    {
        [Test]
        public void DoesntSplitWhenNotNeeded()
        {
            var lines = TextSplit.WidthSplit("Testowe", 33);
            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual("Testowe", lines[0]);
        }

        [Test]
        public void SplitsWithoutNewlines()
        {
            var lines = TextSplit.WidthSplit("TestZXCV", 4);
            Assert.AreEqual(2, lines.Count);
            Assert.AreEqual("Test", lines[0]);
            Assert.AreEqual("ZXCV", lines[1]);
        }

        [Test]
        public void SplitsWithNewlines()
        {
            var lines = TextSplit.WidthSplit("Test\nZXCV", 4);
            Assert.AreEqual(2, lines.Count);
            Assert.AreEqual("Test", lines[0]);
            Assert.AreEqual("ZXCV", lines[1]);
        }

        [Test]
        public void SplitsWith3Newlines()
        {
            var lines = TextSplit.WidthSplit("Test\n\n\nZXCV", 4);
            Assert.AreEqual(4, lines.Count);
            Assert.AreEqual("Test", lines[0]);
            Assert.AreEqual("", lines[1]);
            Assert.AreEqual("", lines[2]);
            Assert.AreEqual("ZXCV", lines[3]);
        }

        [Test]
        public void SplitsMiddleNewlines()
        {
            var lines = TextSplit.WidthSplit("This\nIsTest", 6);
            Assert.AreEqual(2, lines.Count);
            Assert.AreEqual("This", lines[0]);
            Assert.AreEqual("IsTest", lines[1]);
        }
        
    }
}