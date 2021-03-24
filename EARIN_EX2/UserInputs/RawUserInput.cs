using CommandLine;
using EARIN_EX2.Helpers;

namespace EARIN_EX2.UserInputs {
    class RawUserInput {
        [Option('D', "dimensions", Required = true, HelpText = "Specifies dimensionality")]
        public int Dimensions { get; set; }

        [Option('d', "d-num", Required = true, HelpText = "Range of searched integers, [-2^d, 2^d]")]
        public int D { get; set; }

        [Option('a', "a-mat", Required = true, HelpText = "Defines A matrix")]
        public string A { get; set; }

        [Option('b', "b-vec", Required = true, HelpText = "Defines b vector")]
        public string B { get; set; }

        [Option('c', "c-num", Required = true, HelpText = "Defines c real number")]
        public double C { get; set; }

        [Option('p', "population-size", Required = true, HelpText = "Defines population size")]
        public int PopulationSize { get; set; }

        [Option('C', "crossover-prob", Required = true, HelpText = "Defines crossover probability")]
        public double CrossoverProb { get; set; }

        [Option('m', "mutation-prob", Required = true, HelpText = "Defines mutation probability")]
        public double MutationProb { get; set; }

        [Option('i', "iterations", Required = true, HelpText = "Defines number of iterations")]
        public int Iterations { get; set; }

        public UserInput ToUserInput() {
            if (!NDarrayParser.TryParseMatrix(A, out var aMatrix)) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.InvalidInput, $"Incorrect format of A matrix: {A}");
            }

            if (!NDarrayParser.TryParse1DArray(B, out var bVector)) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.InvalidInput, $"Incorrect format of B vector: {B}");
            }

            return new UserInput {
                Dimensions = Dimensions,
                D = D,
                A = aMatrix,
                B = bVector,
                C = C,
                PopulationSize = PopulationSize,
                CrossoverProb = CrossoverProb,
                MutationProb = MutationProb,
                Iterations = Iterations
            };
        }
    }
}
