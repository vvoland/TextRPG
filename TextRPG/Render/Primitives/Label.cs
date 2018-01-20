using System;
using System.Collections.Generic;
using TextRPG.Utils;

namespace TextRPG.Render
{
    public class Label : IRenderable, IColorable, ISizeable
    {
        public Vector2 Position { get; set; }
        public Vector2f Pivot { get; set; }
        public Color Color { get; set; }
        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                _Text = value;
                GenerateRenderText();
            }
        }

        public Vector2 Size
        {
            get
            {
                if(!SizeSet)
                    _Size = CalculateSize();
                return _Size;
            }
            set
            {
                _Size = value;
                SizeSet = true;
                GenerateRenderText();
            }
        }

        public List<string> RenderLines { get; private set; }

        private Vector2 _Size;
        private bool SizeSet = false;
        private string _Text;

        public Label(string text)
            : this(text, Vector2.Zero)
        {
        }

        public Label(string text, Vector2 position)
            : this(text, position, Vector2f.Center)
        {
        }

        public Label(string text, Vector2 position, Vector2f pivot, Color color = Color.White)
        {
            Color = Color.White;
            Text = text;
            Position = position;
            Pivot = pivot;
            Color = color;
        }

        public static implicit operator Label(string text)
        {
            return new Label(text);
        }

        public Vector2 CalculateSize()
        {
            int x = 0;
            int maxX = 0;
            int y = Text.Length == 0 ? 0 : 1;
            foreach(char character in Text)
            {
                if(character == '\n')
                {
                    y++;
                    x = 0;
                }
                else
                {
                    x++;
                    if(x > maxX)
                        maxX = x;
                }
            }

            return new Vector2(maxX, y);
        }

        public void GenerateRenderText()
        {
            RenderLines = TextSplit.WidthSplit(Text, Size.X);
        }

        public void Render(RenderSystem system)
        {
            system.Render(this);
        }
    }
}