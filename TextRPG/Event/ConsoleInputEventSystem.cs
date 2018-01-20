using System;
using TextRPG.Utils;

namespace TextRPG.Event
{
    public class ConsoleInputEventSystem : IEventSystem<InputKeyEvent>
    {
        private static InputKeyEvent EmptyEvent = new InputKeyEvent();

        public bool Get(out InputKeyEvent even, bool block)
        {
            if(!Console.KeyAvailable && !block)
            {
                even = EmptyEvent;
                return false;
            }

            var key = Console.ReadKey(true);

            even = new InputKeyEvent
            {
                Key = ToKey(key.Key),
                KeyChar = key.KeyChar,
                Modifiers = ToModifiers(key.Modifiers)
            };

            return true;
        }

        private KeyModifiers ToModifiers(ConsoleModifiers modifiers)
        {
            KeyModifiers ret = KeyModifiers.None;
            if((modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
                ret |= KeyModifiers.Alt;
            if((modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                ret |= KeyModifiers.Control;
            if((modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift)
                ret |= KeyModifiers.Shift;
            return ret;
        }

        private Key ToKey(ConsoleKey key)
        {
            // Hack, use fact, that internal enum ConsoleKey values are identical
            // to those of Key (by the law of Copy-Paste)
            // so cast it directly to avoid large switch/case ladder
            return (Key)key;
        }
    }
}