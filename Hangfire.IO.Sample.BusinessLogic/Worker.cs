using Hangfire.IO.Sample.BusinessLogic.Hubs;
using System;

namespace Hangfire.IO.Sample.BusinessLogic
{
    public class Worker
    {
        public void DoWork(DateTime queuedAtDateTime)
        {
            var message = string.Format("This Job was queued at {0} and then Processed at {1}", queuedAtDateTime, DateTime.Now);
            NotificationHub.Instance.Notify(message);
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
