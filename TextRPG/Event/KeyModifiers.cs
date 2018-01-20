using System;

namespace TextRPG.Event
{
    [Flags]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Shift = 2,
        Control = 4
    }
}