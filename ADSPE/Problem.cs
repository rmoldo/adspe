using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using JMetalCSharp.Core;
using JMetalCSharp.Utils.Wrapper;
using JMetalCSharp.Utils;
using JMetalCSharp.Encoding.SolutionType;
using System.Xml;

namespace ADSPE
{
    class Problem : JMetalCSharp.Core.Problem
    {

        static String benchmark;
        static int outputNumber = 1;

        public Problem() { }
        /*
        public Problem(string solutionType)
            : this(solutionType, 30)
        {

        }

       
        public Problem(string solutionType, int numberOfVariables)
        {
            NumberOfVariables = numberOfVariables;
            NumberOfObjectives = 2;
            NumberOfConstraints = 0;
            ProblemName = "Problm";

            UpperLimit = new double[NumberOfVariables];
            LowerLimit = new double[NumberOfVariables];

            for (int i = 0; i < NumberOfVariables; i++)
            {
                LowerLimit[i] = 0;
                UpperLimit[i] = 1;
            }

            if (solutionType == "BinaryReal")
            {
                SolutionType = new BinaryRealSolutionType(this);
            }
            else if (solutionType == "Real")
            {
                SolutionType = new RealSolutionType(this);
            }
            else if (solutionType == "ArrayReal")
            {
                SolutionType = new ArrayRealSolutionType(this);
            }
            else
            {
                Console.WriteLine("Error: solution type " + solutionType + " is invalid");
                Logger.Log.Error("Solution type " + solutionType + " is invalid");
                return;
            }
        }

        */
        public override void Evaluate(Solution solution)
        {
            XReal x = new XReal(solution);

            double[] f = new double[NumberOfObjectives];
            f[0] = x.GetValue(0);
            double g = this.EvalG(x);
            double h = this.EvalH(f[0], g);
            f[1] = h * g;

            solution.Objective[0] = f[0];
            solution.Objective[1] = f[1];
        }

        
        private double EvalG(XReal x)
        {
            double g = 0;
            double tmp;
            for (int i = 1; i < x.GetNumberOfDecisionVariables(); i++)
            {
                tmp = x.GetValue(i);
                g += tmp;
            }
            double constant = (9.0 / (NumberOfVariables - 1));
            g = constant * g;
            g = g + 1;
            return g;
        }

        private double EvalH(double f, double g)
        {
            double h = 0;
            h = 1 - Math.Sqrt(f / g);
            return h;
        }

        public XmlWriter GenerateSimulatorConfigFile(Chromosome chromosome)
        {
            using (XmlWriter writer = XmlWriter.Create(@"PSATSim/config.xml"))
            {
                writer.WriteStartElement("psatsim");
                writer.WriteStartElement("config");
                writer.WriteAttributeString("name", "Default");

                // General parameters
                writer.WriteStartElement("general");
                writer.WriteAttributeString("superscalar", chromosome.general.superscalarFactor.ToString());
                writer.WriteAttributeString("rename", chromosome.general.renameEntries.ToString());
                writer.WriteAttributeString("reorder", chromosome.general.reorderEntries.ToString());

                writer.WriteAttributeString("rsb_architecture", chromosome.execution.reservationArchitecture == 0 ? "centralized" :"hybrid");
                writer.WriteAttributeString("rs_per_rsb", chromosome.execution.reservation.ToString());
                writer.WriteAttributeString("speculative", chromosome.memory.branchMisspeculation == 0 ? "false" : "true");

                double speculative = chromosome.memory.speculativeAccuracy / 100;
                writer.WriteAttributeString("speculation_accuracy", speculative.ToString("0.000")); // speculative accuracy with 3 decimal places
                writer.WriteAttributeString("separate_dispatch", chromosome.general.separateDecodeAndDispatch == 0 ? "false" : "true");
                writer.WriteAttributeString("seed", "0");
                writer.WriteAttributeString("trace", benchmark);

                writer.WriteAttributeString("output", "Simulation_Output/output_" + benchmark  + "_" + outputNumber + ".xml");
                writer.WriteAttributeString("vdd", "2.2");
                writer.WriteAttributeString("frequency", "600");
                writer.WriteEndElement();

                // Execution
                writer.WriteStartElement("execution");
                writer.WriteAttributeString("architecture", "complex");
                writer.WriteAttributeString("iadd", chromosome.execution.iaddEU.ToString());
                writer.WriteAttributeString("imult", chromosome.execution.imulEU.ToString());
                writer.WriteAttributeString("idiv", chromosome.execution.idivEU.ToString());
                writer.WriteAttributeString("fpadd", chromosome.execution.fpaddEU.ToString());
                writer.WriteAttributeString("fpmult", chromosome.execution.fpmulEU.ToString());
                writer.WriteAttributeString("fpdiv", chromosome.execution.fpdivEU.ToString());
                writer.WriteAttributeString("fpsqrt", chromosome.execution.fpsqrtEU.ToString());
                writer.WriteAttributeString("branch", chromosome.execution.branchEU.ToString());
                writer.WriteAttributeString("load", chromosome.execution.loadEU.ToString());
                writer.WriteAttributeString("store", chromosome.execution.storeEU.ToString());
                writer.WriteEndElement();

                // Memory
                writer.WriteStartElement("memory");
                writer.WriteAttributeString("architecture", "l2");

                writer.WriteStartElement("l1_code");

                double hitrate = chromosome.memory.l1Code.cacheHitrate / 100;
                writer.WriteAttributeString("hitrate", hitrate.ToString("0.000"));
                writer.WriteAttributeString("latency", chromosome.memory.l1Code.cacheLatency.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("l1_data");

                hitrate = chromosome.memory.l1Data.cacheHitrate / 100;
                writer.WriteAttributeString("hitrate", hitrate.ToString("0.000"));
                writer.WriteAttributeString("latency", chromosome.memory.l1Data.cacheLatency.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("l2");

                hitrate = chromosome.memory.l2.cacheHitrate / 100;
                writer.WriteAttributeString("hitrate", hitrate.ToString("0.000"));
                writer.WriteAttributeString("latency", chromosome.memory.l2.cacheLatency.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("system");
                writer.WriteAttributeString("latency", chromosome.memory.systemMemoryLatency.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();
                return writer;
            }
        }
    }
}
