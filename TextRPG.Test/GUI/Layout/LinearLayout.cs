using NSubstitute;
using NUnit.Framework;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;

namespace TextRPG.Test.GUI.Layout
{
    [TestFixture]
    public class LinearLayoutTest
    {
        GUIWidget Rectangle3, Rectangle5;
        
        [SetUp]
        public void Setup()
        {
            Rectangle3 = new GUIAdapter(new Rectangle(Vector2.One, new Vector2(3, 3)));
            Rectangle5 = new GUIAdapter(new Rectangle(Vector2.One, new Vector2(5, 3)));
        }

        [Test]
        public void RendersItsChildren()
        {
            var t1 = Substitute.For<GUIWidget>();
            var t2 = Substitute.For<GUIWidget>();
            var t3 = Substitute.For<GUIWidget>();
            var fakeRenderer = Substitute.For<RenderSystem>();
            
            var layout = new LinearLayout(LayoutDirection.Horizontal, Vector2.Zero, Vector2f.Center);
            layout.Add(t1);
            layout.Add(t2);
            layout.Add(t3);
            layout.Render(fakeRenderer);

            fakeRenderer.Received().Render(t1);
            fakeRenderer.Received().Render(t2);
            fakeRenderer.Received().Render(t3);
        }

        [Test]
        public void TestCenter()
        {
            var pos = new Vector2(5, 5);
            var size = new Vector2(11, 11);
            var pivot = Vector2f.Center;

            //-----------
            //-----------
            //-----------
            //-----------
            //-xxx-xxxxx-
            //-x-x-C---x-
            //-xxx-xxxxx-
            //-----------
            //-----------
            //-----------
            //-----------


            LinearLayout layout = new LinearLayout(LayoutDirection.Horizontal, pos, pivot);
            layout.Add(Rectangle3);
            layout.Add(Rectangle5);
            layout.Size = size;

            Assert.AreEqual(new Vector2(-3, 0), Rectangle3.Position);
            Assert.AreEqual(new Vector2(2, 0), Rectangle5.Position);
        }

    }
}