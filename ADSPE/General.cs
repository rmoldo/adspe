using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPE
{
    public class General
    {
        public int superscalarFactor;
        public int renameEntries;
        public int reorderEntries;
        public int separateDecodeAndDispatch;

        public string tracePath;
        public string outputPath;

        public General()
        {
            Random random = new Random();

            superscalarFactor = random.Next(1, 17);
            renameEntries = random.Next(1, 513);
            reorderEntries = random.Next(1, 513);

            separateDecodeAndDispatch = random.Next(0, 2);
        }
    }
}
