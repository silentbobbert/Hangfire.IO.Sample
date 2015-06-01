using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelProcessing.BusinessLogic
{
    public class Processor
    {
        private static bool IsPrime(uint n)
        {
            if (n % 2 == 0 && n != 2) return false;
            var root = (uint)Math.Ceiling(Math.Sqrt(n));
            for (uint i = 3; i <= root; i += 2)
            {
                if (n % i == 0 && n != i) return false;
            }
            return true;
        }
        public List<uint> AllPrimesParallelAggregated(uint from, uint to)
        {
            var result = new List<uint>();

            Parallel.For((int)from, (int)to,
                () => new List<uint>(), // Local state initializer
                (i, pls, local) =>      // Loop body
                {
                    if (IsPrime((uint)i))
                    {
                        local.Add((uint)i);
                    }
                    return local;
                },
                local =>                // Local to global state combiner
                {
                    lock (result)
                    {
                        result.AddRange(local);
                    }
                });
            return result;
        }
    }
}
