using System;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.GUI
{
    public class GUIButton : GUIWidget, ISelectable, IActivable
    {
        public override Vector2 Position { get; set; }
        public override Vector2f Pivot
        {
            get => Background.Pivot;
            set
            {
                Background.Pivot = value;
                UpdateLabel();
            }
        }

        public Color SelectionColor = Color.DarkYellow;
        public Color SelectionFrameColor = Color.DarkGreen;
        public bool Selected
        {
            get;
            protected set;
        }
        public override Vector2 Size 
        { 
            get => Background.Size;
            set => Background.Size = value;
        }

        private Frame Background;
        private Label Label;
        private Action ActivateCallback;

        public GUIButton(Vector2 position, string text, Action onActivate)
        {
            Label = new Label(text);
            Label.Color = Color.White;
            Background = new Frame(Vector2.Zero, Vector2.Zero);
            Background.Color = Color.Gray;
            Background.Character = '*';

            Size = Label.CalculateSize().Expand(4, 2);
            Position = position;
            Pivot = Vector2f.Center;
            ActivateCallback = onActivate;
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

        private void UpdateLabel()
        {
            var bounds = BoundsCalculator.Calculate(Vector2.Zero, Pivot, Size);
            Label.Position = bounds.Center;
            Label.Pivot = Vector2f.Center;
        }


        private void SwapColor(IColorable colorable, ref Color color)
        {
            var tmp = colorable.Color;
            colorable.Color = color;
            color = tmp;
        }
    }
}