using System;
using TextRPG.Render;

namespace TextRPG.GUI
{
    public class GUIAdapter : GUIWidget
    {
        private IRenderable Primitive;
        private ISizeable Sizeable;

        public GUIAdapter(IRenderable renderable)

        {
            Sizeable = renderable as ISizeable;
            if(Sizeable == null)
                throw new ArgumentException("GUIAdapter can not adapt an object that does not implement ISizeable interface");
            Primitive = renderable;
        }

        public override Vector2 Position
        { 
            get => Primitive.Position;
            set => Primitive.Position = value;
        }

        public override Vector2f Pivot
        { 
            get => Primitive.Pivot;
            set => Primitive.Pivot = value;
        }

        public override Vector2 Size
        {
            get => Sizeable.Size;
            set => Sizeable.Size = value;
        }

        public override void Render(RenderSystem system)
        {
            Primitive.Render(system);
        }
    }
}