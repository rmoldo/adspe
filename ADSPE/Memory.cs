using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPE
{
    public class Memory
    {
        /*
         * Memory and branching model class for simulator configuration 
         */
        public int memoryArchitecture;
        public int branchMisspeculation;
        public double speculativeAccuracy;
        public double systemMemoryLatency;
        public Cache l1Data;
        public Cache l1Code;
        public Cache l2;

        public Memory(int memoryArchitecture, int branchMisspeculation, double speculativeAccuracy, double systemMemoryLatency, Cache l1Data, Cache l1Code, Cache l2)
        {
            this.memoryArchitecture = memoryArchitecture;
            this.speculativeAccuracy = speculativeAccuracy;
            this.branchMisspeculation = branchMisspeculation;
            this.systemMemoryLatency = systemMemoryLatency;
            this.l1Data = l1Data;
            this.l1Code = l1Code;
            this.l2 = l2;
        }

        public Memory()
        { 
            // Initialize l1 and l2 cache with random values    
            l1Code = new Cache();
            l1Data = new Cache();
            l2 = new Cache();

            Random random = new Random();

            memoryArchitecture = random.Next(0, 3);
            branchMisspeculation = random.Next(0, 2);
            speculativeAccuracy = random.NextDouble() * 100;
            systemMemoryLatency = random.Next(0, 10001);
        }
    }
}
