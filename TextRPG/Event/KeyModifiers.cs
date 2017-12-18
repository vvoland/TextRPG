using System;

namespace TextRPG.Event
{
    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Shift = 2,
        Control = 4
    }
}