namespace TextRPG.Event
{
    public struct InputKeyEvent
    {
        public Key Key { get; set; }
        public KeyModifiers Modifiers { get; set; }
        public char KeyChar { get; set; }
    }
}