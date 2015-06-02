using Hangfire.IO.Sample.BusinessLogic.Annotations;
using Hangfire.IO.Sample.BusinessLogic.Hubs;
using log4net;
using log4net.Config;
using ParallelProcessing.BusinessLogic;
using System;
using System.Diagnostics;

namespace Hangfire.IO.Sample.BusinessLogic
{
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

            var message = string.Format("This Job was queued at {0} and then Processed at {1}. " +
                                        "The task took {2}ms. " +
                                        "The number of primes found was {3}. " +
                                        "The upper limit was {4}", 
                queuedAtDateTime, 
                DateTime.Now,
                s.ElapsedMilliseconds,
                results.Count,
                upperLimit
            );
            NotificationHub.Instance.Notify(message);
            Debug.WriteLine(message);
            Log.Info(message);
        }
    }
}
