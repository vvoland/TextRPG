using System;
using TextRPG.Render;

namespace TextRPG.GUI
{
    public class GUIButton : GUIWidget, ISelectable, IActivable
    {
        public override Vector2 Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
                Background.Position = _Position;
                Label.Position = _Position;
            }
        }
        public override Vector2f Pivot { get; set; }

        private Frame Background;
        private Label Label;
        private Action ActivateCallback;
        private Vector2 _Position;

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
            system.Render(Background);
            Label.Size = Background.Size;
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