using EARIN_EX2.Helpers;
using EARIN_EX2.UserInputs;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX2.GeneticAlg {
    class Population {

        public Population(UserInput userInput) {
            var upperBound = (int)Math.Pow(2, userInput.D);
            var grayHelper = new GrayHelper(upperBound);

            Individuals = new Queue<Individual>(userInput.PopulationSize);

            for (int i = 0; i < userInput.PopulationSize; i++) {
                Individuals.Enqueue(new Individual(grayHelper, userInput));
            }

            SetInitialPositions(upperBound, userInput.Dimensions);
        }

        public Queue<Individual> Individuals { get; set; }
        public int Age { get; set; }

        private void SetInitialPositions(int upperBound, int dimensions) {
            Random random = new Random();

            foreach (var individual in Individuals) {
                var values = Enumerable.Range(0, dimensions).Select(i => random.Next(-upperBound, upperBound)).ToArray();
                individual.X = np.array(values);
            }
        }

        public override string ToString() {
            return string.Join(Environment.NewLine, Individuals.Select((ind, i) => $"{i + 1}:\t{ind}"));
        }
    }
}
