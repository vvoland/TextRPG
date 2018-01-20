using System;
using System.Collections.Generic;
using TextRPG.Event;

namespace TextRPG.GUI
{
    public class GUISystem : IInputEventListener
    {
        public ISelectable CurrentSelectable { get; set; }
        private List<ISelectable> Selectables = new List<ISelectable>();
        private List<GUIWidget> Widgets = new List<GUIWidget>();

        public GUISystem()
        {
            CurrentSelectable = null;
        }

        public void Add(GUIWidget widget)
        {
            ISelectable selectable = widget as ISelectable;
            if(selectable != null)
            {
                AddSelectable(selectable);
            }
            Widgets.Add(widget);
        }

        private void AddSelectable(ISelectable selectable)
        {
            Selectables.Add(selectable);
            if(CurrentSelectable == null)
                Select(selectable);
        }

        private void Select(ISelectable selectable)
        {
            if(CurrentSelectable != null)
                CurrentSelectable.SetSelected(false);

            CurrentSelectable = selectable;
            selectable.SetSelected(true);
        }

        public void NextSelectable()
        {
            if(Selectables.Count == 0)
                return;

            int idx = Selectables.IndexOf(CurrentSelectable) + 1;
            if(idx >= Selectables.Count)
                idx = 0;
            Select(Selectables[idx]);
        }

        public bool OnEvent(InputKeyEvent keyEvent)
        {
            if(keyEvent.Key == Key.Tab)
            {
                NextSelectable();
                return true;
            }

            return false;
        }
    }
}