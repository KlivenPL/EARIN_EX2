using CommandLine;
using EARIN_EX2.GeneticAlg;
using EARIN_EX2.UserInputs;
using FunctionMinimization.Helpers;
using System;

namespace EARIN_EX2 {
    class Program {
        private const string Logo = "+-+-+-+-+-+-+-+-+-+-+\r\n|E|A|R|I|N| |E|X| |2|\r\n+-+-+-+-+-+-+-+-+-+-+\r\nOskar H\u0105cel\r\nMarcin Lisowski\r\nPW, 2021\r\n+-+-+-+-+-+-+-+-+-+-+";

        static void Main(string[] args) {
            PrintLogo();
            GetUserInput(args);
        }

        private static void GetUserInput(string[] args) {
            Parser.Default.ParseArguments<RawUserInput>(args)
                   .WithParsed(o => new Program().Start(o.ToUserInput()));
        }

        private void Start(UserInput userInput) {
            new UserInputValidator(userInput).Validate();

            PrintUserInput(userInput);

            var genAlg = new GeneticAlgorithm(userInput);

            // Just to see initial random population as a reference point.
            genAlg.PrintPopulation();
            genAlg.PrintPopulationStatistics();
            // -=-=-

            genAlg.Evolve();

            genAlg.PrintPopulation();
            genAlg.PrintPopulationStatistics();
        }

        private static void PrintLogo() {
            Console.ForegroundColor = EnumHelper.PickRandom(ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.DarkGray);
            Console.WriteLine(Logo);
            Console.ResetColor();
        }

        private void PrintUserInput(UserInput userInput) {
            Console.WriteLine();
            Console.WriteLine("Given data:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(userInput.ToString());
            Console.ResetColor();
        }

        /*private void DebugGrayCode() {
    var grayHelper = new GrayHelper(8);
    for (int i = 0; i <= 8; i++) {
        var gray = grayHelper.ToGray(i);
        var parsedGray = grayHelper.ToInt(gray);
        System.Console.WriteLine($"i: {i}\tgray: {gray.ToBitString()}\tparsed:{parsedGray}");

        if (i != parsedGray)
            throw new System.Exception();
    }
}*/
    }
}
