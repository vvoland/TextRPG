using System;
using TextRPG.Render;

namespace TextRPG.GUI
{
    public class GUIButton : GUIWidget, ISelectable, IActivable, ISizeable
    {
        public override Vector2 Position { get; set; }
        public override Vector2f Pivot { get; set; }
        public Vector2 Size { get; set; }

        private Frame Background;
        private Label Label;
        private Action ActivateCallback;
        private Vector2 _Position;

        public GUIButton(Vector2 position, string text, Action onActivate)
            : this(position, null, null, onActivate)
        {
            Label = new Label(text);
            Size = Label.CalculateSize().Expand(4, 2);
            Background = new Frame(Vector2.Zero, Size);
            Background.Character = '*';
        }

        public GUIButton(Vector2 position, Frame background, Label label, Action onActivate)
        {
            Background = background;
            Label = label;

            Position = position;
            Pivot = Vector2f.Center;
            ActivateCallback = onActivate;
        }

        public override void Render(RenderSystem system)
        {
            system.PushContext();
                system.Translate(Position);
                system.Render(Background);
                system.Render(Label);
            system.PopContext();
        }

        public void Activate()
        {
            if(ActivateCallback != null)
                ActivateCallback();
        }

        public void SetSelected(bool selected)
        {
            throw new NotImplementedException();
        }
    }
}