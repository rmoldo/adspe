using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPE
{
    public class Execution
    {
        public int executionUnitArchitecture;
        public int reservationArchitecture;

        /*
         *  Number of Execution Units for public int, floats, branch and memory
         */ 
        public int reservation;
        public int integerEU;
        public int floatEU;
        public int branchEU;
        public int memoryEU;
        public int iaddEU;
        public int imulEU;
        public int idivEU;
        public int fpaddEU;
        public int fpmulEU;
        public int fpdivEU;
        public int fpsqrtEU;
        public int loadEU;
        public int storeEU;

        public Execution()
        {
            Random random = new Random();

            reservationArchitecture = random.Next(0, 3); // 0: distributed, 1: centralised, 2: hybrid
            executionUnitArchitecture = random.Next(0, 3); // 0: standard, 1: simple, 2: complex

            reservation = random.Next(1, 9);
            iaddEU = random.Next(1, 9);
            imulEU = random.Next(1, 9);
            idivEU = random.Next(1, 9);
            fpaddEU = random.Next(1, 9);
            fpmulEU = random.Next(1, 9);
            fpdivEU = random.Next(1, 9);
            fpsqrtEU = random.Next(1, 9);
            branchEU = random.Next(1, 9);
            loadEU = random.Next(1, 9);
            storeEU = random.Next(1, 9);
        }

        public Execution(int executionUnitArchitecture, int reservationArchitecture, int reservation, int integerEU, int floatEU, int branchEU, int memoryEU, int iaddEU, int imulEU, int idivEU, int fpaddEU, int fpmulEU, int fpdivEU, int fpsqrtEU, int loadEU, int storeEU)
        {
            this.executionUnitArchitecture = executionUnitArchitecture;
            this.reservationArchitecture = reservationArchitecture;
            this.reservation = reservation;
            this.integerEU = integerEU;
            this.floatEU = floatEU;
            this.branchEU = branchEU;
            this.memoryEU = memoryEU;
            this.iaddEU = iaddEU;
            this.imulEU = imulEU;
            this.idivEU = idivEU;
            this.fpaddEU = fpaddEU;
            this.fpmulEU = fpmulEU;
            this.fpdivEU = fpdivEU;
            this.fpsqrtEU = fpsqrtEU;
            this.loadEU = loadEU;
            this.storeEU = storeEU;
        }
    }
}
