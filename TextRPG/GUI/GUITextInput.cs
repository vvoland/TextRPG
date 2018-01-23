using System;
using System.Linq;
using TextRPG.Event;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.GUI
{
    public class GUITextInput : GUIWidget, ISelectable, IEventListener<InputKeyEvent>
    {
        public string Text
        {
            get;
            private set;
        }
        private Color SelectedColor = Color.DarkGreen;
        private bool Selected = false;
        private Label Label;
        private int MaxLength;
        private char PlaceholderChar;
        private char[] Buffer;
        private int Length = 0;

        public GUITextInput(Vector2 pos, int maxLength = 60, char placeholderChar = '_')
        {
            MaxLength = maxLength;
            PlaceholderChar = placeholderChar;
            Buffer = Enumerable
                .Repeat(placeholderChar, maxLength)
                .ToArray();
            Label = new Label(new string(Buffer), pos);
        }

        public override Vector2 Position
        {
            get => Label.Position;
            set => Label.Position = value;
        }

        public override Vector2f Pivot
        {
            get => Label.Pivot;
            set => Label.Pivot = value;
        }

        public bool OnEvent(InputKeyEvent even)
        {
            if(Selected)
            {
                if(!char.IsControl(even.KeyChar) && Length < MaxLength)
                {
                    Logger.Log("input char: {0}", even.KeyChar);
                    Buffer[Length] = even.KeyChar;
                    Length++;
                    UpdateText();
                    return true;
                }
                else if(even.Key == Key.Backspace && Length > 0)
                {
                    Logger.Log("backspacing", even.KeyChar);
                    Buffer[--Length] = PlaceholderChar;
                    UpdateText();
                    return true;
                }
            }

            return false;
        }

        private void UpdateText()
        {
            Label.Text = new string(Buffer);
            Text = new string(Buffer, 0, Length);
        }

        public override void Render(RenderSystem system)
        {
            system.Render(Label);
        }

        public void SetSelected(bool selected)
        {
            var tmp = Label.Color;
            Label.Color = SelectedColor;
            SelectedColor = tmp;
            Selected = selected;
        }
    }
}