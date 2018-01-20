using NUnit.Framework;
using NSubstitute;
using TextRPG.GUI;
using TextRPG.Render;

namespace TextRPG.Test.GUI
{
    class MockButton : GUIWidget, ISelectable
    {
        public override Vector2 Position { get; set; }
        public override Vector2f Pivot { get; set; }

        public bool Selected { get; set; }

        public MockButton()
        {
            Position = Vector2.Zero;
            Pivot = Vector2f.Center; 
        }

        public override void Render(RenderSystem system)
        {
            throw new System.NotImplementedException();
        }

        public void SetSelected(bool selected)
        {
            Selected = selected;
        }
    }

    [TestFixture]
    public class GUISystemTest
    {
        MockButton Button, Button2;
        GUISystem System;

        [SetUp]
        public void Setup()
        {
            System = new GUISystem();
            Button = new MockButton();
            Button2 = new MockButton();
        }

        [Test]
        public void AllowsCyclingThroughSelectableWidgets()
        {
            System.Add(Button);
            System.Add(Button2);

            Assert.AreEqual(Button, System.CurrentSelectable);
            // Cycle 30 times
            for(int i = 0; i < 30; i++)
            {
                System.NextSelectable();
                Assert.AreEqual(Button2, System.CurrentSelectable);
                System.NextSelectable();
                Assert.AreEqual(Button, System.CurrentSelectable);
            }
        }

        [Test]
        public void CyclingSelectsSelectableWidgets()
        {
            System.Add(Button);
            System.Add(Button2);

            // Cycle 30 times
            for(int i = 0; i < 30; i++)
            {
                Assert.That(Button.Selected);
                Assert.That(!Button2.Selected);
                System.NextSelectable();
                Assert.That(!Button.Selected);
                Assert.That(Button2.Selected);
                System.NextSelectable();
            }
        }
    }
}