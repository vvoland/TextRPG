using System;

namespace TextRPG.Utils
{
    public static class ConsoleColorConversion
    {
        // Color's underlying enum values are identical to ConsoleColor
        // Use that to avoid large switch-case ladder
        // ALTHOUGH THIS IS BAD!!! (but colors won't ever change so...)

        public static ConsoleColor ToConsoleColor(this Color color)
        {
            return (ConsoleColor)color;
        }

        public static Color FromConsoleColor(ConsoleColor color)
        {
            return (Color)color;
        }
    }
}