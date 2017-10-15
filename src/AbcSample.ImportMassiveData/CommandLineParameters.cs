using CommandLine;

namespace AbcSample.ImportMassiveData
{
    public class CommandLineParameters
    {
        [Option('i', "input", HelpText = "Path input file to import country in alpha 3 format", Required = true)]
        public string InputFilePath { get; set; }

    }
}