using System.Linq;
using NUnit.Framework;
using TextRPG.Utils;

namespace TextRPG.Test.Utils
{
    [TestFixture(10, 10)]
    [TestFixture(32, 32)]
    [TestFixture(4, 4)]
    public class FakeConsoleTest
    {
        private FakeConsole Console;
        private int Width, Height;

        public FakeConsoleTest(int width, int height)
        {
            Width = width;
            Height = height;
        }

        [SetUp]
        public void Setup()
        {
            Console = new FakeConsole(Width, Height);
        }

        [Test]
        public void WindowWidthEqualsBufferWidth()
        {
            Assert.AreEqual(Width, Console.WindowWidth);
        }


        [Test]
        public void WindowHeightEqualsBufferHeight()
        {
            Assert.AreEqual(Height, Console.WindowHeight);
        }

        [Test]
        public void BufferIsFilledWithNullCharactersByDefault()
        {
            CollectionAssert.AreEqual(Console.Buffer, Enumerable.Repeat(0, Width * Height));
        }

        [Test]
        public void CanWriteToAnyPosition()
        {
            Console.SetCursorPosition(0, 1);
            Console.Write('A');
            Assert.AreEqual('A', Console.BufferAt(0, 1));
        }

        [Test]
        public void When_TryingToSetCursorOutOfBounds_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => Console.SetCursorPosition(Width, Height));
        }

    }
}