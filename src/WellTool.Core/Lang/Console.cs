using System;
using System.IO;

namespace WellTool.Core.Lang
{
    public static class Console
    {
        public static void Print(string message)
        {
            System.Console.Write(message);
        }

        public static void Println(string message = "")
        {
            System.Console.WriteLine(message);
        }

        public static void Println(object obj)
        {
            System.Console.WriteLine(obj);
        }

        public static void Println(int value)
        {
            System.Console.WriteLine(value);
        }

        public static void Println(long value)
        {
            System.Console.WriteLine(value);
        }

        public static void Println(double value)
        {
            System.Console.WriteLine(value);
        }

        public static void Println(bool value)
        {
            System.Console.WriteLine(value);
        }

        public static string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public static char ReadKey()
        {
            return System.Console.ReadKey().KeyChar;
        }

        public static void Clear()
        {
            System.Console.Clear();
        }

        public static void SetTitle(string title)
        {
            System.Console.Title = title;
        }

        public static void SetForeGroundColor(ConsoleColor color)
        {
            System.Console.ForegroundColor = (System.ConsoleColor)color;
        }

        public static void SetBackgroundColor(ConsoleColor color)
        {
            System.Console.BackgroundColor = (System.ConsoleColor)color;
        }

        public static void ResetColor()
        {
            System.Console.ResetColor();
        }
    }

    public enum ConsoleColor
    {
        Black,
        DarkBlue,
        DarkGreen,
        DarkCyan,
        DarkRed,
        DarkMagenta,
        DarkYellow,
        Gray,
        DarkGray,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Yellow,
        White
    }
}