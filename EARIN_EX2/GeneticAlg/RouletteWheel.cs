using System;

namespace EARIN_EX2.GeneticAlg {
    class RouletteWheel {

        private readonly double[] probabilities;
        private readonly Random random;

        public RouletteWheel(double[] probabilities) {
            this.probabilities = probabilities;
            random = new Random();
        }

        public int Spin() {
            var rand = random.NextDouble();
            double currentVal = 0;
            int i = 0;

            while (currentVal < rand) {
                currentVal += probabilities[i];

                if (currentVal >= rand)
                    break;
                i++;
            }

            return i;
        }
    }
}
