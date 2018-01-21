using NUnit.Framework;

namespace TextRPG.Test.Base
{
    [TestFixture]
    public class RectTest
    {
        static object[] ExampleRectangles = new object[]
        {
            new int[] {0, 0, 10, 10},
            new int[] {0, 0, 1, 1},
            new int[] {0, 0, 0, 0},
            new int[] {-10, 0, -10, 10},
            new int[] {0, -10, -10, 10},
        };

        [Test]
        [TestCase(0, 0, 10, 10)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(-10, 0, -10, 10)]
        [TestCase(0, -10, -10, 10)]
        public void CreateWithTopLeftAndSize(int posX, int posY, int width, int height)
        {
            var rect = new Rect(posX, posY, width, height);
            Assert.AreEqual(rect.XMin, posX);
            Assert.AreEqual(rect.YMin, posY);
            Assert.AreEqual(rect.XMax, posX + width);
            Assert.AreEqual(rect.YMax, posY + height);
            Assert.AreEqual(rect.Position.X, posX);
            Assert.AreEqual(rect.Position.Y, posY);
            Assert.AreEqual(rect.X, posX);
            Assert.AreEqual(rect.Y, posY);
            Assert.AreEqual(rect.Width, width);
            Assert.AreEqual(rect.Height, height);

            Assert.AreEqual((int)(posX + (width / 2.0f)), rect.Center.X);
            Assert.AreEqual((int)(posY + (height / 2.0f)), rect.Center.Y);
        }

    }
}