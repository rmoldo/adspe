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
using System.IO;
using System.Diagnostics;

namespace ADSPE
{
    class Problem : JMetalCSharp.Core.Problem
    {
        static String benchmark;
        static String outputFilePath = "";
        static int outputNumber = 1;

        public Problem(string bench)
            : this(25, bench)
        {
            benchmark = bench;
        }
       
        public Problem(int numberOfVariables, String bench)
        {
            NumberOfVariables = numberOfVariables;
            NumberOfObjectives = 2;
            NumberOfConstraints = 0;
            ProblemName = "Problem";

            UpperLimit = new double[NumberOfVariables];
            LowerLimit = new double[NumberOfVariables];

            for (int i = 0; i < NumberOfVariables; i++)
            {
                switch(i)
                {
                    // superscalar  1-16
                    case 0:
                        LowerLimit[i] = 1;
                        UpperLimit[i] = 16;
                        break;
                    // rename
                    case 1:
                        LowerLimit[i] = 1;
                        UpperLimit[i] = 512;
                        break;
                    // reorderEntries
                    case 2:
                        LowerLimit[i] = 1;
                        UpperLimit[i] = 512;
                        break;
                    // reservationArchitecture:  centralized, hybrid
                    case 3:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 1;
                        break;
                    // reservation
                    case 4:
                        LowerLimit[i] = 1;
                        UpperLimit[i] = 8;
                        break;
                    // branchMisspeculation
                    case 5:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 1;
                        break;
                    // speculativeAccuracy
                    case 6:
                        LowerLimit[i] = 0.0;
                        UpperLimit[i] = 100.0;
                        break;
                    // separateDecodeAndDispatch
                    case 7:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 1;
                        break;
                    // l1Code.cacheHitrate
                    case 18:
                        LowerLimit[i] = 0.0;
                        UpperLimit[i] = 100.0;
                        break;
                    // l1Code.cacheLatency
                    case 19:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 100;
                        break;
                    // l1Data.cacheHitrate
                    case 20:
                        LowerLimit[i] = 0.0;
                        UpperLimit[i] = 100.0;
                        break;
                    // l1Data.cacheLatency
                    case 21:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 100;
                        break;
                    // l2.cacheHitrate
                    case 22:
                        LowerLimit[i] = 0.0;
                        UpperLimit[i] = 100.0;
                        break;
                    // l2.cacheLatency
                    case 23:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 100;
                        break;
                    // systemMemoryLatency
                    case 24:
                        LowerLimit[i] = 0;
                        UpperLimit[i] = 10000;
                        break;
                    //  iadd, imul, idiv, fpadd, fpmul, fpdiv, fpsqrt, branch, load, store
                    default:
                        LowerLimit[i] = 1;
                        UpperLimit[i] = 8;
                        break;
                }
            }

            
            SolutionType = new ArrayRealSolutionType(this);
        }

        public override void Evaluate(Solution solution)
        {
            XReal x = new XReal(solution);

            double[] configuration = new double[NumberOfVariables];
            double[] objectives = new double[NumberOfObjectives];

            for (int i = 0; i < NumberOfVariables; ++i)
            {
                configuration[i] = x.GetValue(i);
            }

            Chromosome chromosome = new Chromosome();
            
            // Write PSATSim custom configuration  
            XmlWriter writer = GenerateSimulatorConfigFile(chromosome);
            writer.Close();

            StartSimulator();

            // Get cpi and energy from output file
            double cpi = 1/ getIPC();
            double energy = getEnergy();

            // Set new objectives
            solution.Objective[0] = cpi;
            solution.Objective[1] = energy;
        }

        public XmlWriter GenerateSimulatorConfigFile(Chromosome chromosome)
        {
            using (XmlWriter writer = XmlWriter.Create(@"PSATSim\config.xml"))
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

                outputFilePath = "Simulation_Output/output_" + outputNumber++ + ".xml";

                writer.WriteAttributeString("output", outputFilePath);
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

        private double getEnergy()
        {
            string path = Directory.GetCurrentDirectory() + "/PSATsim/" + outputFilePath;
            long length = new System.IO.FileInfo(path).Length;

            if (length != 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("general");
                return Convert.ToDouble(nodes[0].Attributes.GetNamedItem("energy").InnerText);
            }
            else
            {
                return 0;
            }
        }

        private double getIPC()
        {
            string path = Directory.GetCurrentDirectory() + "/PSATsim/" + outputFilePath;
            long length = new System.IO.FileInfo(path).Length;

            if (length != 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("general");
                return Convert.ToDouble(nodes[0].Attributes.GetNamedItem("ipc").InnerText);
            }
            else
            {
                return 0;
            }
        }

        public Chromosome CreateChromosomeFromSolution(double[] solution)
        {
            Chromosome chromosome = new Chromosome();

            // General
            chromosome.general.superscalarFactor = Convert.ToInt32(solution[0]);
            chromosome.general.renameEntries = Convert.ToInt32(solution[1]);
            chromosome.general.reorderEntries = Convert.ToInt32(solution[2]);
            chromosome.execution.reservationArchitecture = Convert.ToInt32(solution[3]);
            chromosome.execution.reservation = Convert.ToInt32(solution[4]);
            chromosome.memory.branchMisspeculation = Convert.ToInt32(solution[5]);
            chromosome.memory.speculativeAccuracy = solution[6];
            chromosome.general.separateDecodeAndDispatch = Convert.ToInt32(solution[7]);

            // Execution
            chromosome.execution.iaddEU = Convert.ToInt32(solution[8]);
            chromosome.execution.imulEU = Convert.ToInt32(solution[9]);
            chromosome.execution.idivEU = Convert.ToInt32(solution[10]);
            chromosome.execution.fpaddEU = Convert.ToInt32(solution[11]);
            chromosome.execution.fpmulEU = Convert.ToInt32(solution[12]);
            chromosome.execution.fpdivEU = Convert.ToInt32(solution[13]);
            chromosome.execution.fpsqrtEU = Convert.ToInt32(solution[14]);
            chromosome.execution.branchEU = Convert.ToInt32(solution[15]);
            chromosome.execution.loadEU = Convert.ToInt32(solution[16]);
            chromosome.execution.storeEU = Convert.ToInt32(solution[17]);

            // Memory
            chromosome.memory.l1Code.cacheHitrate = solution[18];
            chromosome.memory.l1Code.cacheLatency = Convert.ToInt32(solution[19]);
            chromosome.memory.l1Data.cacheHitrate = solution[20];
            chromosome.memory.l1Data.cacheLatency = Convert.ToInt32(solution[21]);
            chromosome.memory.l2.cacheHitrate = solution[22];
            chromosome.memory.l2.cacheHitrate = Convert.ToInt32(solution[23]);
            chromosome.memory.systemMemoryLatency = Convert.ToInt32(solution[24]);

            return chromosome;
        }

        public void StartSimulator()
        {
            Process process = new Process();
            string path = Directory.GetCurrentDirectory() + @"\PSATSim";

            var StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = path,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = Directory.GetCurrentDirectory() + @"\PSATSim\psatsim_con.exe",
                Arguments = "config.xml " + outputFilePath + " " + " -g",
                RedirectStandardInput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            process.StartInfo = StartInfo;
            process.Start();

            process.WaitForExit();
        }
    }
}