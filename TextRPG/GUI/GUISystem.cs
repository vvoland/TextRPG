using System;
using System.Collections.Generic;
using TextRPG.Event;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.GUI
{
    public class GUISystem : IEventListener<InputKeyEvent>
    {
        public ISelectable CurrentSelectable { get; set; }

        private List<ISelectable> Selectables = new List<ISelectable>();
        private List<IEventListener<InputKeyEvent>> InputKeyListeners = new List<IEventListener<InputKeyEvent>>();
        private List<GUIWidget> Widgets = new List<GUIWidget>();

        public GUISystem()
        {
            CurrentSelectable = null;
        }

        public void Add(GUIWidget widget)
        {
            ISelectable selectable = widget as ISelectable;
            IEventListener<InputKeyEvent> keyListener = widget as IEventListener<InputKeyEvent>;

            if(selectable != null)
            {
                AddSelectable(selectable);
            }
            if(keyListener != null)
            {
                InputKeyListeners.Add(keyListener);
            }

            Widgets.Add(widget);
        }


        public void Remove(GUIWidget widget)
        {
            ISelectable selectable = widget as ISelectable;
            IEventListener<InputKeyEvent> keyListener = widget as IEventListener<InputKeyEvent>;

            if(selectable != null)
            {
                RemoveSelectable(selectable);
            }
            if(keyListener != null)
            {
                InputKeyListeners.Remove(keyListener);
            }
            Widgets.Remove(widget);
        }

        private void RemoveSelectable(ISelectable selectable)
        {
            Selectables.Remove(selectable);
            NextSelectable();
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
            {
                CurrentSelectable = null;
                return;
            }

            int idx = Selectables.IndexOf(CurrentSelectable) + 1;
            if(idx >= Selectables.Count)
                idx = 0;
            Select(Selectables[idx]);
        }

        public void Activate()
        {
            if(CurrentSelectable != null)
            {
                var activable = CurrentSelectable as IActivable;
                if(activable != null)
                {
                    activable.Activate();
                }
            }
        }

        public bool OnEvent(InputKeyEvent keyEvent)
        {
            if (keyEvent.Key == Key.Tab)
            {
                NextSelectable();
                return true;
            }
            else if (keyEvent.Key == Key.Enter)
            {
                Activate();
                return true;
            }
            bool consumed = PropagateEvents(keyEvent);

            return consumed;
        }

        private bool PropagateEvents(InputKeyEvent keyEvent)
        {
            foreach (var listener in InputKeyListeners)
            {
                if (listener.OnEvent(keyEvent))
                    return true;
            }

            return false;
        }
    }
}