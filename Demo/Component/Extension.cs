using System;
using System.Drawing;

namespace Demo.Component
{
    public static class Extension
    {

        public static Color GetColor(this ConsoleColor consoleColor)
        {
            switch (consoleColor)
            {
                case ConsoleColor.Black:
                    return Color.Black;
                case ConsoleColor.DarkBlue:
                    return Color.DarkBlue;
                case ConsoleColor.DarkGreen:
                    return Color.DarkGreen;
                case ConsoleColor.DarkCyan:
                    return Color.DarkCyan;
                case ConsoleColor.DarkRed:
                    return Color.DarkRed;
                case ConsoleColor.DarkMagenta:
                    return Color.DarkMagenta;
                case ConsoleColor.DarkYellow:
                    return Color.DarkGoldenrod;
                case ConsoleColor.Gray:
                    return Color.Gray;
                case ConsoleColor.DarkGray:
                    return Color.White;
                case ConsoleColor.Blue:
                    return Color.Blue;
                case ConsoleColor.Green:
                    return Color.Green;
                case ConsoleColor.Cyan:
                    return Color.Cyan;
                case ConsoleColor.Red:
                    return Color.Red;
                case ConsoleColor.Magenta:
                    return Color.Magenta;
                case ConsoleColor.Yellow:
                    return Color.Gold;
                case ConsoleColor.White:
                    return Color.White;
                default:
                    return Color.White;
            }
        }

    }
}
