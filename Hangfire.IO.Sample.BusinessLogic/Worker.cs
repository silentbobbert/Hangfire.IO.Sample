using System.Linq;
using Hangfire.IO.Sample.BusinessLogic.Annotations;
using Hangfire.IO.Sample.BusinessLogic.Hubs;
using log4net;
using Newtonsoft.Json;
using ParallelProcessing.BusinessLogic;
using System;
using System.Diagnostics;

namespace Hangfire.IO.Sample.BusinessLogic
{
    public struct PrimeNumberSearchResult
    {
        public DateTime Queued { get; set; }
        public DateTime Processed { get; set; }
        public Int64 ProcessingTime { get; set; }
        public int NumberOfPrimes { get; set; }
        public UInt64 MaxPrime { get; set; }
        public uint UpperLimit { get; set; }
    }
    [UsedImplicitly]
    public class Worker : IWorker
    {
        private ILog Log { get; set; }

        public Worker(ILog log)
        {
            Log = log;
        }

        public void DoWork(DateTime queuedAtDateTime, uint upperLimit)
        {
            var s = new Stopwatch();
            s.Start();

            var processor = new Processor();
            var results = processor.AllPrimesParallelAggregated(0, upperLimit);

            s.Stop();

            var result = new PrimeNumberSearchResult
            {
                Queued = queuedAtDateTime,
                Processed = DateTime.Now,
                ProcessingTime = s.ElapsedMilliseconds,
                NumberOfPrimes = results.Count,
                MaxPrime = results.Max(),
                UpperLimit = upperLimit
            };

            var message = string.Format("This Job was queued at {0} and then Processed at {1}. " +
                                        "The task took {2}ms. " +
                                        "The number of primes found was {3}. " +
                                        "The upper limit was {4}. The largest prime was  {5}",
                result.Queued , 
                result.Processed,
                result.ProcessingTime,
                result.NumberOfPrimes,
                result.UpperLimit,
                result.MaxPrime
            );

            NotificationHub.Instance.Notify(message);
            NotificationHub.Instance.Notify(JsonConvert.SerializeObject(result));
            Debug.WriteLine(message);
            Log.Info(message);
        }
    }
}
