using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AbcSample.DAL;
using AbcSample.Entities;
using CommandLine;
using Microsoft.SqlServer.Server;

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

    }

    class Program
    {
        static void Main(string[] args)
        {
            CommandLineParameters options = new CommandLineParameters();
            if (Parser.Default.ParseArgumentsStrict(args, options))
            {
                var task = ProcessFile(options.InputFilePath);
                task.Wait();
            }
        }

        public static async Task ProcessFile(string inputFileName)
        {
            ColorConsole.WriteLineWhite("Loading matching corner cases...");

            if (string.IsNullOrEmpty(inputFileName))
            {
                throw new ArgumentNullException(nameof(inputFileName));
            }

            if (!File.Exists(inputFileName))
            {
                throw new FileNotFoundException("The file with seasons was not found", inputFileName);
            }

            List<string> matchingLines = File.ReadAllLines(inputFileName).ToList();
            //Delete titles
            matchingLines.RemoveAt(0);
            IList<Country> countriesToUpload = new List<Country>();
            foreach (var matchingLine in matchingLines)
            {
                try
                {
                    var country = MapLine2Country(matchingLine);
                    countriesToUpload.Add(country);
                }
                catch (Exception e)
                {
                    ColorConsole.WriteLineSucess(e.Message);
                }
            }

            ICountryRepository countryRepository = new CountryRepository();
            await countryRepository.Upsert(countriesToUpload);
        }

        private const int ExpectedColumnNumber = 3;
        private const int CountryIdPosition = 1;
        private const int CountryNamePosition = 0;

        private static Country MapLine2Country(string line)
        {
            string[] items = line.Split(',');

            if (items.Length != ExpectedColumnNumber)
            {
                throw new ApplicationException($"The line {line} does not follow the expected format.");
            }

            string id = items[CountryIdPosition];
            string countryName = items[CountryNamePosition];


            return new Country
            {
                Description = countryName,
                Id = id
            };
        }
    }

    public class CommandLineParameters
    {
        [Option('i', "input", HelpText = "Path input file to import country in alpha 3 format", Required = true)]
        public string InputFilePath { get; set; }

    }
}
