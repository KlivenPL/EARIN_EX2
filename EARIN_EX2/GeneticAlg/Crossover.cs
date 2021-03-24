using System;
using System.Collections;
using System.Linq;

namespace EARIN_EX2.GeneticAlg {
    public class Crossover {
        private readonly Random random;
        private readonly double crossoverProb;
        private readonly int dimensions;

        public Crossover(double crossoverProb, int dimensions) {
            random = new Random();
            this.crossoverProb = crossoverProb;
            this.dimensions = dimensions;
        }

        public bool TryCrossover(Individual parentA, Individual parentB, out BitArray[] newGeneA, out BitArray[] newGeneB) {
            newGeneA = null;
            newGeneB = null;

            if (random.NextDouble() > crossoverProb)
                return false;

            var oneValueBitLength = parentA.GrayedX.First().Length;
            var totalGenotypeLength = oneValueBitLength * dimensions;

            int cut = random.Next(1, totalGenotypeLength);

            newGeneA = new BitArray[dimensions];
            newGeneB = new BitArray[dimensions];

            var oldGeneA = parentA.GrayedX;
            var oldGeneB = parentB.GrayedX;

            int tmpLength = 0;
            for (int i = 0; i < dimensions; i++) {
                newGeneA[i] = new BitArray(oneValueBitLength);
                newGeneB[i] = new BitArray(oneValueBitLength);

                for (int j = 0; j < oneValueBitLength; j++) {
                    newGeneA[i][j] = tmpLength < cut ? oldGeneA[i][j] : oldGeneB[i][j];
                    newGeneB[i][j] = tmpLength < cut ? oldGeneB[i][j] : oldGeneA[i][j];

                    tmpLength++;
                }
            }

            return true;
        }
    }
}
