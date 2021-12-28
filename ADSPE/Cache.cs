using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPE
{

    public class Cache
    {
        /*
         * Cache model class for defining simulator configuration parameter
         */

        public int cacheLatency;
        public double cacheHitrate;

        public Cache()
        {
            /* 
             * Generate a random cache configuration 
             */

            Random random = new Random();
            this.cacheLatency = random.Next(0, 101);
            this.cacheHitrate = random.NextDouble() * 100;
        }

        public Cache(int cacheLatency, double cacheHitrate)
        {
            this.cacheLatency = cacheLatency;
            this.cacheHitrate = cacheHitrate;
        }
    }
}
