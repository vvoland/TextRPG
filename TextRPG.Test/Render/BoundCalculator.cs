using NUnit.Framework;
using TextRPG.Render;

namespace TextRPG.Test.Render
{
    [TestFixture]
    public class BoundCalculatorTest
    {
        private BoundCalculator Bounds;

        [SetUp]
        public void Setup()
        {
            Bounds = new BoundCalculator();
        }

        //        x  y  pivot x, y, w  h  sx  ex  sy  ey
        [TestCase(4, 4, 0.5f, 0.5f, 3, 3,  3,  6,  3, 6)] // center
        [TestCase(4, 4, 0.5f, 0.5f, 1, 1,  4,  5,  4, 5)] // center
        [TestCase(4, 4, 0.0f, 0.0f, 3, 3,  4,  7,  4, 7)] // upper left
        [TestCase(4, 4, 1.0f, 0.0f, 3, 3,  2,  5,  4, 7)] // upper right
        [Test]
        public void CorrectlyCalculatesBounds(
            int posx, int posy,
            float pivotx, float pivoty,
            int sizex, int sizey,
            int startX, int endX,
            int startY, int endY)
        {
            Vector2 size = new Vector2(sizex, sizey);
            Vector2 pos = new Vector2(posx, posy);
            Vector2f pivot = new Vector2f(pivotx, pivoty);

            Rect rect = Bounds.Calculate(pos, pivot, size);

            Assert.AreEqual(startX, rect.XMin, "StartX");
            Assert.AreEqual(endX, rect.XMax, "EndX");
            Assert.AreEqual(startY, rect.YMin, "StartY");
            Assert.AreEqual(endY, rect.YMax, "EndY");
        }
    }
}