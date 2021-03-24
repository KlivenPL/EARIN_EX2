using EARIN_EX2.UserInputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX2.GeneticAlg {
    public class GeneticAlgorithm {
        private readonly UserInput userInput;
        private readonly Crossover crossover;
        private readonly Mutation mutation;
        private readonly Population population;

        public GeneticAlgorithm(UserInput userInput) {
            this.userInput = userInput;

            crossover = new Crossover(userInput.CrossoverProb, userInput.Dimensions);
            mutation = new Mutation(userInput.MutationProb, userInput.Dimensions);
            population = new Population(userInput);
        }

        public void Evolve() {
            for (int i = 0; i < userInput.Iterations; i++) {
                var fitnesses = GetPopulationFitness(out var minFitness, out var maxFitness);
                var probs = GetSelectionProbabilities(fitnesses, minFitness, maxFitness);

                var rouletteWheel = new RouletteWheel(probs);

                List<BitArray[]> newGenotypes = new List<BitArray[]>();

                for (int j = 0; j < userInput.PopulationSize / 2; j++) {
                    var parentA = population.Individuals.ElementAt(rouletteWheel.Spin());
                    var parentB = population.Individuals.ElementAt(rouletteWheel.Spin());

                    if (crossover.TryCrossover(parentA, parentB, out var newGeneA, out var newGeneB)) {
                        newGenotypes.Add(newGeneA);
                        newGenotypes.Add(newGeneB);
                    }
                }

                foreach (var gene in newGenotypes) {
                    var oldIndividual = population.Individuals.Dequeue();
                    oldIndividual.GrayedX = gene;
                    population.Individuals.Enqueue(oldIndividual);
                }

                foreach (var individual in population.Individuals) {
                    mutation.TryMutate(individual);
                }

                population.Age++;
            }
        }

        public void PrintPopulation() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Population {population.Age}:");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine(population.ToString());
            Console.WriteLine();
        }

        public void PrintPopulationStatistics() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Population {population.Age} statistics:");
            Console.ResetColor();
            Console.WriteLine($"Population average fitness: {GetPopulationFitness(out var minFitness, out var maxFitness).Average()}");
            Console.WriteLine($"Population min fitness: {minFitness}");
            Console.WriteLine($"Population max fitness: {maxFitness}");
            Console.WriteLine();
        }

        private IEnumerable<double> GetPopulationFitness(out double minFitness, out double maxFitness) {
            var fitnesses = population.Individuals.Select(individual => individual.Fitness);

            maxFitness = fitnesses.Max();
            minFitness = fitnesses.Min();

            return fitnesses;
        }

        private double[] GetSelectionProbabilities(IEnumerable<double> populationFitness, double minFitness, double maxFitness) {
            var qPrimes = populationFitness.Select(fitness => (fitness - minFitness) / (maxFitness - minFitness));
            var qPrimesSum = qPrimes.Sum();

            var probs = qPrimes.Select(qPrime => qPrime / qPrimesSum).ToArray();

            return probs;
        }
    }
}
