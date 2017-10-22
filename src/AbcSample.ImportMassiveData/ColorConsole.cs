using System;

namespace AbcSample.ImportMassiveData
{
    internal static class ColorConsole
    {
        private static readonly object consoleLock = new object();
        public static void WriteMessageSucess(string message)
        {
            lock (consoleLock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = previousColor;
            }
        }
        public static void WriteLineNormal(string message)
        {
            lock (consoleLock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(message);
                Console.ForegroundColor = previousColor;
            }
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