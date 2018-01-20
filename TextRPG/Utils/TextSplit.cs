using System.Collections.Generic;

namespace TextRPG.Utils
{
    public static class TextSplit
    {
        public static List<string> WidthSplit(string text, int width, char delimiter = '\n')
        {
            int curInRow = 0;
            List<string> lines = new List<string>();

            string line = "";
            for(int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if(c != delimiter)
                {
                    if(curInRow == width)
                    {
                        FlushLine(lines, ref curInRow, ref line);
                    }
                    line += text[i];
                    curInRow++;
                }
                
                if(c == delimiter || i == text.Length - 1)
                {
                    FlushLine(lines, ref curInRow, ref line);
                }
            }

            return lines;
        }

        private static void FlushLine(List<string> lines, ref int curInRow, ref string line)
        {
            lines.Add(line);
            curInRow = 0;
            line = "";
        }
    }
}