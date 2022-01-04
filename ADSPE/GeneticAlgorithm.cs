using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMetalCSharp.Core;
using JMetalCSharp.Operators.Crossover;
using JMetalCSharp.Operators.Mutation;
using JMetalCSharp.Operators.Selection;
using JMetalCSharp.Utils;

namespace ADSPE
{
    public class GeneticAlgorithm
    {
        private int populationSize;
        private int generationSize;
        private double crossoverProbability;
        private String benchmarkPath;
        private String selectionType;

        public GeneticAlgorithm(int populationSize, int generationSize, double crossoverProbability, string benchmarkPath, string selectionType)
        {
            this.populationSize = populationSize;
            this.generationSize = generationSize;
            this.crossoverProbability = crossoverProbability;
            this.benchmarkPath = benchmarkPath;
            this.selectionType = selectionType;
        }

        public void Start()
        {
            Operator crossover;
            Operator mutation;
            Operator selection;
            Problem problem = new Problem(benchmarkPath);
            Dictionary<String, object> parameters;

            Algorithm algorithm = new JMetalCSharp.Metaheuristics.SMSEMOA.SMSEMOA(problem);

            // Set algorithm parameters
            algorithm.SetInputParameter("populationSize", populationSize);
            algorithm.SetInputParameter("maxEvaluations", generationSize * populationSize);

            // Mutation and crossover for real codification
            parameters = new Dictionary<string, object>();
            parameters.Add("probability", crossoverProbability);
            parameters.Add("distributionIndex", 20.0);
            crossover = CrossoverFactory.GetCrossoverOperator("SBXCrossover", parameters);

            parameters = new Dictionary<string, object>();
            parameters.Add("probability", 1.0 / problem.NumberOfVariables);
            parameters.Add("distributionIndex", 20.0);
            mutation = MutationFactory.GetMutationOperator("PolynomialMutation", parameters);

            // Selection operator
            parameters = null;
            selection = SelectionFactory.GetSelectionOperator(selectionType, parameters);


            // Add the operators to the algorithm
            algorithm.AddOperator("crossover", crossover);
            algorithm.AddOperator("mutation", mutation);
            algorithm.AddOperator("selection", selection);

            // Execute the algorithm
            SolutionSet population = algorithm.Execute();

            // Save results to their coresponding file
            population.PrintObjectivesToFile("FUN"); // objective values
            population.PrintVariablesToFile("VAR"); // variables values
        }
    }
}
