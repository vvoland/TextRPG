using System.Collections.Generic;

namespace TextRPG.Render
{
    public abstract class RenderSystem
    {
        protected Stack<RenderSystemContext> Contexts = new Stack<RenderSystemContext>();
        protected RenderSystemContext Context = new RenderSystemContext();

        public RenderSystem()
        {
        }

        public virtual void PopContext()
        {
            if(Contexts.Count == 0)
                throw new RenderException("No more contexts to pop");
            Context = Contexts.Pop();
        }

        public virtual void PushContext()
        {
            Contexts.Push(Context.Clone());
        }

        public virtual void SetClipping(Vector2 size)
        {
            Context.Clipping = size;
        }

        public virtual void DisableClipping()
        {
            Context.Clipping = new Vector2(0, 0);
        }

        public virtual void Translate(Vector2 translation)
        {
            Context.Translation += translation;
        }

        public abstract void Render(IRenderable renderable);
        public abstract void Render(Rectangle rect);
        public abstract void Render(Frame frame);
        public abstract void Render(Label label);
        public abstract void Flush();
        public abstract void Clear(Rect rect);
    }

}