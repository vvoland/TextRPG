using System;
using System.Collections.Generic;
using System.Linq;
using TextRPG.Render;

namespace TextRPG.GUI.Layout
{
    public class LinearLayout : GUIWidget, ISizeable
    {
        public override Vector2 Size
        {
            get => _Size;
            set
            {
                _Size = value;
                Recalculate();
            }
        }
        public override Vector2 Position { get; set; }
        public override Vector2f Pivot
        {
            get
            {
                return _Pivot;
            }
            set
            {
                _Pivot = value;
                Widgets.ForEach(w => w.Pivot = value);
            }
        }
        public int Spacing = 1;

        private Vector2 _Size;
        private Vector2f _Pivot;
        private List<GUIWidget> Widgets = new List<GUIWidget>();
        private LayoutDirection Direction;
        private Rect Bounds;

        public LinearLayout(LayoutDirection dir, Vector2 pos, Vector2f pivot)
        {
            Direction = dir;
            Position = pos;
            Pivot = pivot;
        }

        private Vector2 CalculateTotalSize()
        {
            int width, height;
            width = height = 0;
            int max = 0;

            if(Widgets.Count == 0)
                return Vector2.Zero;

            if(Direction == LayoutDirection.Horizontal)
            {
                foreach(var widget in Widgets)
                {            
                    width += widget.Size.X + Spacing;
                    if(widget.Size.Y > max)
                    {
                        max = widget.Size.Y;
                    }
                }
                width -= Spacing;
                return new Vector2(width, max);
            }
            else
            {
                foreach(var widget in Widgets)
                {
                    height += widget.Size.Y + Spacing;
                    if(widget.Size.X > max)
                    {
                        max = widget.Size.X;
                    }
                }
                height -= Spacing;
                return new Vector2(max, height);
            }
        }

        private void Recalculate()
        {
            var totalSize = CalculateTotalSize();
            Bounds = BoundsCalculator.Calculate(Vector2.Zero, Pivot, totalSize);
            int cur = GetCoordinateBasedOnDirection(Bounds.Min);

            foreach(var widget in Widgets)
            {
                int size = GetCoordinateBasedOnDirection(widget.Size);
                cur += size / 2;
                if(Direction == LayoutDirection.Horizontal)
                    widget.Position = new Vector2(cur, Bounds.Center.Y);
                else
                    widget.Position = new Vector2(Bounds.Center.X, cur);
                cur += (size + 1) / 2 + Spacing;
            }
        }

        private int GetCoordinateBasedOnDirection(Vector2 vec)
        {
            if(Direction == LayoutDirection.Horizontal)
                return vec.X;
            else
                return vec.Y;
        }

        public override void Render(RenderSystem system)
        {
            system.PushContext();
            system.Translate(Position);
            Widgets.ForEach(w => system.Render(w));
            system.PopContext();
        }

        public void Add(GUIWidget widget)
        {
            Widgets.Add(widget);
            widget.Pivot = Pivot;
            Recalculate();
        }

        public void Remove(GUIWidget widget)
        {
            Widgets.Remove(widget);
            Recalculate();
        }
    }
}