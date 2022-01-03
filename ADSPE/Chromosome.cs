using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPE
{
    public class Chromosome
    {
        public Execution execution;
        public Memory memory;
        public General general;

        public Chromosome()
        {
            execution = new Execution();
            memory = new Memory();
            general = new General();
        }
    }
}
