using System;
using System.Linq;

namespace EARIN_EX2.GeneticAlg {
    public class Mutation {
        private readonly Random random;
        private readonly double mutationProb;
        private readonly int dimensions;

        public Mutation(double mutationProb, int dimensions) {
            random = new Random();
            this.mutationProb = mutationProb;
            this.dimensions = dimensions;
        }

        public void TryMutate(Individual individual) {
            if (random.NextDouble() > mutationProb) {
                return;
            }

            var oneValueBitLength = individual.GrayedX.First().Length;
            var genotype = individual.GrayedX;

            int i = random.Next(0, dimensions);
            int j = random.Next(0, oneValueBitLength);

            genotype[i][j] = !genotype[i][j];
        }
    }
}
