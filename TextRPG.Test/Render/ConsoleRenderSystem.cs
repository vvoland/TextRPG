using NSubstitute;
using NUnit.Framework;
using TextRPG.Render;
using TextRPG.Utils;
using System;
using System.Linq;

namespace TextRPG.Test
{

    [TestFixture]
    public class ConsoleRenderSystemTest
    {
        ConsoleRenderSystem Renderer9x9;
        FakeConsole Console9x9;

        [SetUp]
        public void Setup()
        {
            Console9x9 = new FakeConsole(9, 9);
            Renderer9x9 = new ConsoleRenderSystem(Console9x9, 9, 9);
        }

        [Test]
        public void RendersFrame()
        {
            Frame line = new Frame(new Vector2(4, 4), new Vector2(5, 5));
            Renderer9x9.Render(line);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, line.Character, @"
                ---------
                ---------
                --xxxxx--
                --x---x--
                --x---x--
                --x---x--
                --xxxxx--
                ---------
                ---------");
        }

        [Test]
        public void RendersLabel()
        {
            Label line = new Label("Test", new Vector2(4, 4));
            Renderer9x9.Render(line);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, Char.MinValue, @"
                ---------
                ---------
                ---------
                ---------
                --Test---
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void RendersLabelWithOverflow()
        {
            Label line = new Label("TestyX", new Vector2(4, 4));
            line.Size = new Vector2(5, 2);
            Renderer9x9.Render(line);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, Char.MinValue, @"
                ---------
                ---------
                ---------
                --Testy--
                ----X----
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void RendersMultilineLabel()
        {
            Label line = new Label("Test\nTest\nTest", new Vector2(4, 4));
            Renderer9x9.Render(line);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, Char.MinValue, @"
                ---------
                ---------
                ---------
                --Test---
                --Test---
                --Test---
                ---------
                ---------
                ---------");
        }        

        [TestCase(0.5f, 0.5f)]
        [TestCase(0.5f, 0.0f)]
        [TestCase(0.5f, 1.0f)]
        [Test]
        public void Renders5x1Rectangle(float pivotX, float pivotY)
        {
            Frame line = new Frame(new Vector2(4, 4), new Vector2(5, 1));
            line.Pivot = new Vector2f(pivotX, pivotY);
            Renderer9x9.Render(line);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, line.Character, @"
                ---------
                ---------
                ---------
                ---------
                --xxxxx--
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void Renders1x1Rectangle()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(1, 1));
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ---------
                ---------
                ----x----
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void Renders5x5RectangleWithCenterPivot()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(5, 5));
            rectangle.Pivot = new Vector2f(0.5f, 0.5f);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                --xxxxx--
                --xxxxx--
                --xxxxx--
                --xxxxx--
                --xxxxx--
                ---------
                ---------");

        }

        [Test]
        public void Renders3x3RectangleWithCenterPivot()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(3, 3));
            rectangle.Pivot = new Vector2f(0.5f, 0.5f);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ---------
                ---xxx---
                ---xxx---
                ---xxx---
                ---------
                ---------
                ---------");

        }

        [Test]
        public void Renders3x3RectangleWithUpperLeftPivot()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(3, 3));
            rectangle.Pivot = new Vector2f(0, 0);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ---------
                ---------
                ----xxx--
                ----xxx--
                ----xxx--
                ---------
                ---------");
        }

        [Test]
        public void Renders3x3RectangleWithUpperRightPivot()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(3, 3));
            rectangle.Pivot = new Vector2f(1, 0);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ---------
                ---------
                --xxx----
                --xxx----
                --xxx----
                ---------
                ---------");
        }

        [Test]
        public void Renders3x3RectangleWithLowerRightPivot()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(3, 3));
            rectangle.Pivot = new Vector2f(1, 1);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                --xxx----
                --xxx----
                --xxx----
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void Renders3x3RectangleWithLowerLeftPivot()
        {
            Rectangle rectangle = new Rectangle(new Vector2(4, 4), new Vector2(3, 3));
            rectangle.Pivot = new Vector2f(0, 1);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ----xxx--
                ----xxx--
                ----xxx--
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void Renders3x3RectangleClippedByRendererBounds()
        {
            Rectangle rectangle = new Rectangle(new Vector2(8, 3), new Vector2(3, 3));
            rectangle.Pivot = new Vector2f(0, 0);
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ---------
                --------x
                --------x
                --------x
                ---------
                ---------
                ---------");
        }

        [Test]
        public void Renders3x3RectangleClippedTo2x2ByClippingBounds()
        {
            Rectangle rectangle = new Rectangle(new Vector2(1, 1), new Vector2(4, 4));
            rectangle.Pivot = new Vector2f(0, 0);
            Renderer9x9.SetClipping(new Vector2(3, 3));
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                -xx------
                -xx------
                ---------
                ---------
                ---------
                ---------
                ---------
                ---------");
        }

        [Test]
        public void RendersTranslated3x3RectangleClippedTo2x2ByClippingBounds()
        {
            Rectangle rectangle = new Rectangle(new Vector2(1, 1), new Vector2(4, 4));
            rectangle.Pivot = new Vector2f(0, 0);
            Renderer9x9.Translate(new Vector2(2, 2));
            Renderer9x9.SetClipping(new Vector2(3, 3));
            Renderer9x9.Render(rectangle);
            Renderer9x9.Flush();
            CheckBuffer(Console9x9, rectangle.Character, @"
                ---------
                ---------
                ---------
                ---xx----
                ---xx----
                ---------
                ---------
                ---------
                ---------");
        }

        private void CheckBuffer(FakeConsole console, char character, string buffer)
        {
            buffer = SanitizeBuffer(buffer, character);
            Assert.AreEqual(console.Width * console.Height, buffer.Length, "SanitizeBuffer did sth wrong!");
            Assert.That(
                console.BufferEquals(buffer),
                string.Format("Actual:\n{0}\nExpected:\n{1}", 
                    MakeReadable(new string(console.Buffer), character, console.Width), MakeReadable(buffer, character, console.Width)
                )
            );
        }

        private string SanitizeBuffer(string buf, char fillChar)
        {
            return buf
                .Replace(" ", "")
                .Replace("\t", "")
                .Replace("-", " ")
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace('x', fillChar);
        }

        private string MakeReadable(string buf, char fillChar, int width)
        {
            buf = buf
                .Replace(" ", "-")
                .Replace(fillChar, 'x');

            string pattern = ".{" + width.ToString() + "}";
            return System.Text.RegularExpressions.Regex.Replace(buf, pattern, "$0\n");
        }
    }
}