using System;

namespace AbcSample.ImportMassiveData
{
    internal static class ColorConsole
    {
        private static readonly object consoleLock = new object();
        public static void WriteMessageSucess(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void WriteLineNormal(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteLineSucess(object value)
        {
            lock (consoleLock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(value);
                Console.ForegroundColor = previousColor;
            }
        }

        public static void WriteLineError(object value)
        {
            lock (consoleLock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(value);
                Console.ForegroundColor = previousColor;
            }
        }

        public static void WriteLineWhite(object value)
        {
            lock (consoleLock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(value);
                Console.ForegroundColor = previousColor;
            }
        }

        public static void WriteSucess(object value)
        {
            lock (consoleLock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(value);
                Console.ForegroundColor = previousColor;
            }
        }
    }
}