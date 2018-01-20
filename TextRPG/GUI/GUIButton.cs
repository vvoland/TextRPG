using System;
using TextRPG.Render;

namespace TextRPG.GUI
{
    public class GUIButton : GUIWidget, ISelectable, IActivable, ISizeable
    {
        public override Vector2 Position { get; set; }
        public override Vector2f Pivot { get; set; }
        public Vector2 Size { get; set; }
        public Color SelectionColor = Color.DarkYellow;
        public Color SelectionFrameColor = Color.DarkGreen;
        public bool Selected
        {
            get;
            protected set;
        }

        private Frame Background;
        private Label Label;
        private Action ActivateCallback;
        private Vector2 _Position;

        public GUIButton(Vector2 position, string text, Action onActivate)
            : this(position, null, null, onActivate)
        {
            Label = new Label(text);
            Label.Color = Color.White;
            Size = Label.CalculateSize().Expand(4, 2);
            Background = new Frame(Vector2.Zero, Size);
            Background.Color = Color.Gray;
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
            if(selected == !Selected)
            {
                SwapColor(Background, ref SelectionFrameColor);
                SwapColor(Label, ref SelectionColor);
            }
            Selected = selected;
        }

        private void SwapColor(IColorable colorable, ref Color color)
        {
            var tmp = colorable.Color;
            colorable.Color = color;
            color = tmp;
        }
    }
}