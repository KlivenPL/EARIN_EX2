using Numpy;
using System.Text;

namespace EARIN_EX2.UserInputs {
    public class UserInput {
        public int Dimensions { get; set; }
        public int D { get; set; }
        public NDarray A { get; set; }
        public NDarray B { get; set; }
        public double C { get; set; }
        public int PopulationSize { get; set; }
        public double CrossoverProb { get; set; }
        public double MutationProb { get; set; }
        public int Iterations { get; set; }

        public override string ToString() {
            var sb = new StringBuilder();

            sb.AppendLine($"Dimensions: {Dimensions}");
            sb.AppendLine($"d: {D}");
            sb.AppendLine($"A");
            sb.AppendLine($"{A}");
            sb.AppendLine($"B: {B}");
            sb.AppendLine($"C: {C}");

            sb.AppendLine($"Crossover probability: {CrossoverProb}");
            sb.AppendLine($"Mutation probability: {MutationProb}");
            sb.AppendLine($"Iterations: {Iterations}");

            return sb.ToString();
        }
    }
}
