using Hangfire.IO.Sample.BusinessLogic.Hubs;
using ParallelProcessing.BusinessLogic;
using System;
using System.Diagnostics;

namespace Hangfire.IO.Sample.BusinessLogic
{
    public class Worker
    {
        public void DoWork(DateTime queuedAtDateTime, int numberOfItems)
        {
            var s = new Stopwatch();
            s.Start();

            var processor = new Processor(numberOfItems);
            processor.DoWorkInParallel();

            s.Stop();

            var message = string.Format("This Job was queued at {0} and then Processed at {1}. " +
                                        "The task took {2}ms. " +
                                        "The number of items processed was {3}", 
                queuedAtDateTime, 
                DateTime.Now,
                s.ElapsedMilliseconds,
                numberOfItems
            );
            NotificationHub.Instance.Notify(message);
            Debug.WriteLine(message);
        }
    }
}
