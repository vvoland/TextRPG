using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TextRPG.Render;

namespace TextRPG.Test.Render
{
    [TestFixture]
    public class ConsoleRenderBufferTest
    {
        private ConsoleRenderBuffer Buffer1, Buffer2;
        private ConsoleRenderBuffer BufferDiff1, BufferDiff2;
        private const int BufferSize = 5;

        ConsoleRenderBuffer CreateBuffer()
        {
            return new ConsoleRenderBuffer(BufferSize, BufferSize, ' ', ConsoleColor.White);
        }

        [SetUp]
        public void Setup()
        {
            Buffer1 = CreateBuffer();
            Buffer2 = CreateBuffer();
            BufferDiff1 = CreateBuffer();
            BufferDiff2 = CreateBuffer();

            BufferDiff1.Set(3, 3, 'x', ConsoleColor.Blue);
            BufferDiff2.Set(3, 3, 'x', ConsoleColor.Blue);

            for(int i = 0; i < BufferSize; i++)
            {
                BufferDiff1.Set(i, 0, 'x', ConsoleColor.White);
                BufferDiff2.Set(0, i, 'x', ConsoleColor.White);
            }
        }

        [Test]
        public void GetAndSetTest()
        {
            Buffer1.Set(1, 1, 'x', ConsoleColor.Red);
            Assert.AreEqual('x', Buffer1.Get(1, 1));
            Assert.AreEqual(ConsoleColor.Red, Buffer1.GetColor(1, 1));
        }

        [Test]
        public void ClearTest()
        {
            for(int x = 0; x < BufferSize; x++)
            for(int y = 0; y < BufferSize; y++)
                Buffer1.Set(x, y, (char)('a' + x + y), ConsoleColor.White);
            Buffer1.Clear();

            for(int x = 0; x < BufferSize; x++)
            for(int y = 0; y < BufferSize; y++)
                Assert.AreEqual(' ', Buffer1.Get(x, y));
        }

        [Test]
        public void DiffTest()
        {
            var diff = BufferDiff1.Diff(BufferDiff2);
            var expected = new List<int>();
            for(int i = 1; i < BufferSize; i++)
            {
                expected.Add(i); // x=i, y=0
                expected.Add(i * BufferSize); // x=0, y=i
            }
            diff.Sort();
            expected.Sort();

            Assert.That(Enumerable.SequenceEqual(diff, expected));
        }

        [Test]
        public void DiffOrderDoesntMatter()
        {
            var diff12 = BufferDiff1.Diff(BufferDiff2);
            var diff21 = BufferDiff2.Diff(BufferDiff1);
            Assert.That(Enumerable.SequenceEqual(diff12, diff21));
        }
    }
}