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
        public void Test()
        {
            var pos = new Vector2(5, 5);
            var size = new Vector2(11, 11);
            var pivot = Vector2f.Center;

            //-----------
            //-----------
            //-----------
            //-----------
            //-xxx-xxxxx-
            //-x-xCx---x-
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
            Assert.AreEqual(new Vector2(1, 0), Rectangle5.Position);
        }
    }
}