using NUnit.Framework;
using TextRPG.Render;

namespace TextRPG.Test.Render
{
    [TestFixture]
    public class LabelTest
    {

        [TestCase("", 0, 0)]
        [TestCase("x", 1, 1)]
        [TestCase("  Test  ", 4+2+2, 1)]
        [TestCase("  Test  \n  ", 4+2+2, 2)]
        [Test]
        public void CorrectlyEstimatesSize(string text, int expectedWidth, int expectedHeight)
        {
            var label = new Label(text);
            var size = label.CalculateSize();
            Assert.AreEqual(expectedWidth, size.X);
            Assert.AreEqual(expectedHeight, size.Y);
        }
    }
}